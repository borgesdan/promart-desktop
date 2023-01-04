using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promart.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    RG = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Certidao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResponsibleName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ResponsiblePhone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Relationship = table.Column<byte>(type: "tinyint", nullable: false),
                    IsGovernmentBeneficiary = table.Column<bool>(type: "bit", nullable: true),
                    Dwelling = table.Column<byte>(type: "tinyint", nullable: false),
                    MonthlyIncome = table.Column<byte>(type: "tinyint", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Complement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ReferencePoint = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SchoolName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SchoolYear = table.Column<byte>(type: "tinyint", nullable: false),
                    SchoolShift = table.Column<byte>(type: "tinyint", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    ProjectShift = table.Column<byte>(type: "tinyint", nullable: false),
                    Registry = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RegistryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workshops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentRelationships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Relationship = table.Column<byte>(type: "tinyint", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Education = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Income = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRelationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentRelationships_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentWorkshop",
                columns: table => new
                {
                    StudentsId = table.Column<int>(type: "int", nullable: false),
                    WorkshopsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentWorkshop", x => new { x.StudentsId, x.WorkshopsId });
                    table.ForeignKey(
                        name: "FK_StudentWorkshop_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentWorkshop_Workshops_WorkshopsId",
                        column: x => x.WorkshopsId,
                        principalTable: "Workshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentRelationships_StudentId",
                table: "StudentRelationships",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentWorkshop_WorkshopsId",
                table: "StudentWorkshop",
                column: "WorkshopsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentRelationships");

            migrationBuilder.DropTable(
                name: "StudentWorkshop");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Workshops");
        }
    }
}
