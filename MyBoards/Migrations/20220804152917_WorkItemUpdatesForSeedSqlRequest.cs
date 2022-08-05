using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoards.Migrations
{
    public partial class WorkItemUpdatesForSeedSqlRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemainingWork",
                table: "WorkItems",
                newName: "RemaningWork");

            migrationBuilder.RenameColumn(
                name: "IterationPath",
                table: "WorkItems",
                newName: "Iteration_Path");

            migrationBuilder.RenameColumn(
                name: "Effort",
                table: "WorkItems",
                newName: "Efford");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemaningWork",
                table: "WorkItems",
                newName: "RemainingWork");

            migrationBuilder.RenameColumn(
                name: "Iteration_Path",
                table: "WorkItems",
                newName: "IterationPath");

            migrationBuilder.RenameColumn(
                name: "Efford",
                table: "WorkItems",
                newName: "Effort");
        }
    }
}
