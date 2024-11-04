using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationAndTrainingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class deletedstar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stars",
                table: "Courses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stars",
                table: "Courses",
                type: "int",
                nullable: true);
        }
    }
}
