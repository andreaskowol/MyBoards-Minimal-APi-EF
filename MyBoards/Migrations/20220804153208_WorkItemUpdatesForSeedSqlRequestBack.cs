using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoards.Migrations
{
    public partial class WorkItemUpdatesForSeedSqlRequestBack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemaningWork",
                table: "WorkItems",
                newName: "RemainingWork");

            migrationBuilder.RenameColumn(
                name: "Iteration_Path",
                table: "WorkItems",
                newName: "IterationPath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemainingWork",
                table: "WorkItems",
                newName: "RemaningWork");

            migrationBuilder.RenameColumn(
                name: "IterationPath",
                table: "WorkItems",
                newName: "Iteration_Path");
        }
    }
}
