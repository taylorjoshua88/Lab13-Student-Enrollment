using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StudentEnrollment.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseLevel = table.Column<int>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Iteration = table.Column<string>(maxLength: 30, nullable: true),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Technology = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentCourseID = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    HighestCourseLevel = table.Column<int>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    PassedInterview = table.Column<bool>(nullable: false),
                    Placed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Student_Course_CurrentCourseID",
                        column: x => x.CurrentCourseID,
                        principalTable: "Course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_CurrentCourseID",
                table: "Student",
                column: "CurrentCourseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Course");
        }
    }
}
