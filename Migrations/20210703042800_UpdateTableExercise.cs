using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlanner.Migrations
{
    public partial class UpdateTableExercise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Difficulty",
                table: "Exercises",
                newName: "Sets");

            migrationBuilder.AddColumn<int>(
                name: "Reps",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reps",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "Sets",
                table: "Exercises",
                newName: "Difficulty");
        }
    }
}
