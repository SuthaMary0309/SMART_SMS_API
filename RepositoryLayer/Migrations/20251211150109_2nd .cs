using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class _2nd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Students_StudentID",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Students_StudentID1",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Parents_ParentID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Classes_ClassID",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Teachers_TeacherID",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ClassID",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TeacherID",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Students_AdmissionNo",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_Email",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ParentID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_PhoneNo",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Parents_Email",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Parents_PhoneNo",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Marks_StudentID1",
                table: "Marks");

            migrationBuilder.DropColumn(
                name: "AdmissionNo",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentID1",
                table: "Marks");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherID",
                table: "Subjects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNo",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNo",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Students_StudentID",
                table: "Marks",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Students_StudentID",
                table: "Marks");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherID",
                table: "Subjects",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNo",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AdmissionNo",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Students",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentID",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNo",
                table: "Parents",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Parents",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentID1",
                table: "Marks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ClassID",
                table: "Subjects",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TeacherID",
                table: "Subjects",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AdmissionNo",
                table: "Students",
                column: "AdmissionNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ParentID",
                table: "Students",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_PhoneNo",
                table: "Students",
                column: "PhoneNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_Email",
                table: "Parents",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parents_PhoneNo",
                table: "Parents",
                column: "PhoneNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Marks_StudentID1",
                table: "Marks",
                column: "StudentID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Students_StudentID",
                table: "Marks",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Students_StudentID1",
                table: "Marks",
                column: "StudentID1",
                principalTable: "Students",
                principalColumn: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Parents_ParentID",
                table: "Students",
                column: "ParentID",
                principalTable: "Parents",
                principalColumn: "ParentID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Classes_ClassID",
                table: "Subjects",
                column: "ClassID",
                principalTable: "Classes",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Teachers_TeacherID",
                table: "Subjects",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "TeacherID");
        }
    }
}
