using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class thMary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Marks_ExamID",
                table: "Marks",
                column: "ExamID");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Exams_ExamID",
                table: "Marks",
                column: "ExamID",
                principalTable: "Exams",
                principalColumn: "ExamID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Exams_ExamID",
                table: "Marks");

            migrationBuilder.DropIndex(
                name: "IX_Marks_ExamID",
                table: "Marks");
        }
    }
}
