using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoards.Migrations
{
    public partial class viewTopAuthorsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
create view View_TopAuthors as 
select top 5 Users.FullName, count(*) as WorkItemsCreated
from Users
join WorkItems on WorkItems.AuthorId = Users.Id
group by users.id, users.FullName
order by WorkItemsCreated desc
"
);
       }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
drop view View_TopAuthors
"
);
        }
    }
}
