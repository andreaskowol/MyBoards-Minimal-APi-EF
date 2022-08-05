using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using MyBoards.DTO;
using MyBoards.entities;
using System.Linq.Expressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Prevent
//System.Text.Json.JsonException: A possible object cycle was detected. This can either be due to a cycle or if the object depth is larger than the maximum allowed depth of 64. Consider using ReferenceHandler.Preserve on JsonSerializerOptions to support cycles
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<MyBoardContext>(
    option => option
    //.UseLazyLoadingProxies()
    .UseSqlServer(builder.Configuration.GetConnectionString("MyBoardsConnectionString"))
    ); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<MyBoardContext>();

var pendingMigrations = dbContext.Database.GetPendingMigrations();
if (pendingMigrations.Any())
{
    dbContext.Database.Migrate();
}

var users = dbContext.Users.ToList();
if (!users.Any())
{
    var user1 = new User()
    {
        Email = "email1@test.com",
        FullName = "User One",
        Address = new Address()
        {
            City = "Warszawa",
            Street = "Szeroka"
        }
    };

    var user2 = new User()
    {
        Email = "email2@test.com",
        FullName = "User Two",
        Address = new Address()
        {
            City = "Kraków",
            Street = "D³uga"
        }
    };

    dbContext.Users.AddRange(user1, user2);

    dbContext.SaveChanges();
}

app.MapGet("data", async (MyBoardContext db) =>
{
    var user = await db.Users
    .AsNoTracking()
    .Include(u => u.Comments).ThenInclude(c => c.WorkItem)
    .Include(u => u.Address)
    .FirstAsync(u => u.Id == Guid.Parse("68366DBE-0809-490F-CC1D-08DA10AB0E61"));

    return user;
});

app.MapGet("pagination", (MyBoardContext db) =>
{
    // user input
    var filter = "a";
    var sortBy = "FullName"; // "FullName" "Email" null
    bool sortByDesc = false;
    int pageNumber = 1;
    int pageSize = 10;
    //

    var query = db.Users
        .Where(u => filter == null || 
        (u.Email.ToLower().Contains(filter.ToLower()) || (u.FullName.ToLower().Contains(filter.ToLower()))));

    var totalCount = query.Count();

    if(sortBy != null)
    {
        var columnsSelector = new Dictionary<string, Expression<Func<User, object>>>
        {
            {nameof(User.Email), user => user.Email },
            {nameof(User.FullName), user => user.Email },
        };

        var sortByExpression = columnsSelector[sortBy];

        query = sortByDesc ? query.OrderByDescending(sortByExpression) : query.OrderBy(sortByExpression);
    }

    var result = query.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

    var pagedResult = new PagedResult<User>(result, totalCount, pageSize, pageNumber);

    return pagedResult;

});

app.MapGet("select", async (MyBoardContext db) =>
{
    var comments = await db.Users
        .Include(u => u.Address)
        .Include(u => u.Comments)
        .Where(u => u.Address.Country == "Albania")
        .SelectMany(u => u.Comments)
        .Select(c => c.Message)
        .ToListAsync();

    return comments;
});

app.MapGet("rawGet", (MyBoardContext db) =>
{
    var minWorkItemsCount = "85";

    var states = db.WorkItemStates
    .FromSqlInterpolated ($@"
select WorkItemStates.Id, WorkItemStates.Value
from WorkItemStates
join WorkItems on WorkItems.StateId = WorkItemStates.Id
group by WorkItemStates.id, WorkItemStates.Value
having count(*) > {minWorkItemsCount}
")
    .ToList();

    return states;
});

app.MapGet("execSql", (MyBoardContext db) =>
{
    db.Database.ExecuteSqlRaw(@"
update Comments
set UpdatedDate = getdate()
where AuthorId = '2073271A-3DFC-4A63-CBE5-08DA10AB0E61'
");

});

app.MapGet("getView", async (MyBoardContext db) =>
{
    var topAuthots = await db.ViewTopAuthors.ToListAsync();
    return topAuthots;
});

app.MapPost("update", async (MyBoardContext db) =>
{
    var epic = await db.Epics.FirstAsync(epic => epic.Id == 1);
    var onHoldState = await db.WorkItemStates.FirstAsync(x => x.Value == "On Hold"); // find proper state 

    epic.Area = "Updated area";
    epic.Priority = 1;
    epic.State = onHoldState;
    epic.StartDate = DateTime.Now;

    await db.SaveChangesAsync();
    return epic;
});

app.MapPost("create", async (MyBoardContext db) =>
{
    var address = new Address()
    {
        City = "NY",
        Country = "US",
        Street = "7th"
    };

    var user = new User()
    {
        Email = "test@test.com",
        FullName = "User Test",
        Address = address
    };

    db.Users.Add(user);
    await db.SaveChangesAsync();

    return user;
});

app.MapDelete("delete", async (MyBoardContext db) =>
{
    var user = await db.Users
    .Include(u => u.Comments)
    .FirstAsync(u => u.Id == Guid.Parse("DC231ACF-AD3C-445D-CC08-08DA10AB0E61"));
    
    db.Users.Remove(user);
    await db.SaveChangesAsync();
});

/*app.MapGet("lazyLoading", async (MyBoardContext db) =>
{
    var withAddress = true;

    var user = await db.Users
    .FirstAsync(u => u.Id == Guid.Parse("EBFBD70D-AC83-4D08-CBC6-08DA10AB0E61"));

    if (withAddress)
    {
        var result = new { FullName = user.FullName, Address = $"{user.Address.Street} {user.Address.City}" };
        return result;
    }
    return new {FullName = user.FullName, Address = "-"};
    
});*/

app.Run();
