using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promart.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Students",
                newName: "ProjectStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectStatus",
                table: "Students",
                newName: "Status");
        }
    }
}
