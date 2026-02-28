using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam_Management.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "SubjectMsts",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectMsts", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "ExamMasters",
                columns: table => new
                {
                    MasterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    ExamYear = table.Column<int>(type: "int", nullable: false),
                    TotalMark = table.Column<int>(type: "int", nullable: false),
                    PassOrFail = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamMasters", x => x.MasterID);
                    table.ForeignKey(
                        name: "FK_ExamMasters_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamDtls",
                columns: table => new
                {
                    DtlsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterID = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false),
                    Marks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamDtls", x => x.DtlsID);
                    table.ForeignKey(
                        name: "FK_ExamDtls_ExamMasters_MasterID",
                        column: x => x.MasterID,
                        principalTable: "ExamMasters",
                        principalColumn: "MasterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamDtls_SubjectMsts_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "SubjectMsts",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamDtls_MasterID",
                table: "ExamDtls",
                column: "MasterID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamDtls_SubjectID",
                table: "ExamDtls",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamMasters_StudentID",
                table: "ExamMasters",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Mail",
                table: "Students",
                column: "Mail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamDtls");

            migrationBuilder.DropTable(
                name: "ExamMasters");

            migrationBuilder.DropTable(
                name: "SubjectMsts");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
