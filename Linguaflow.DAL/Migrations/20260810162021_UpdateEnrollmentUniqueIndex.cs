using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linguaflow.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEnrollmentUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Tutors_TutorId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_StudentId_CourseId",
                table: "Enrollments");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId_CourseId_TutorId",
                table: "Enrollments",
                columns: new[] { "StudentId", "CourseId", "TutorId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Tutors_TutorId",
                table: "Enrollments",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Tutors_TutorId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_StudentId_CourseId_TutorId",
                table: "Enrollments");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId_CourseId",
                table: "Enrollments",
                columns: new[] { "StudentId", "CourseId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Tutors_TutorId",
                table: "Enrollments",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
