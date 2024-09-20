using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLMSProject.Migrations
{
    /// <inheritdoc />
    public partial class CourseInstructorMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Instructors_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "InstructorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Instructors_InstructorId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Courses");
        }
    }
}
