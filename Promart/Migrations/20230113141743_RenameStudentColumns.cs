using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promart.Migrations
{
    /// <inheritdoc />
    public partial class RenameStudentColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistryDate",
                table: "Students",
                newName: "ProjectRegistryDate");

            migrationBuilder.RenameColumn(
                name: "Registry",
                table: "Students",
                newName: "ProjectRegistry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectRegistryDate",
                table: "Students",
                newName: "RegistryDate");

            migrationBuilder.RenameColumn(
                name: "ProjectRegistry",
                table: "Students",
                newName: "Registry");
        }
    }
}
