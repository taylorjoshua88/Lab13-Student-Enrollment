using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StudentEnrollment.Migrations
{
    public partial class currentCourseId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Course_CurrentCourseID",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "CurrentCourseID",
                table: "Student",
                newName: "CurrentCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_CurrentCourseID",
                table: "Student",
                newName: "IX_Student_CurrentCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Course_CurrentCourseId",
                table: "Student",
                column: "CurrentCourseId",
                principalTable: "Course",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Course_CurrentCourseId",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "CurrentCourseId",
                table: "Student",
                newName: "CurrentCourseID");

            migrationBuilder.RenameIndex(
                name: "IX_Student_CurrentCourseId",
                table: "Student",
                newName: "IX_Student_CurrentCourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Course_CurrentCourseID",
                table: "Student",
                column: "CurrentCourseID",
                principalTable: "Course",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
