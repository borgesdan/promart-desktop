using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promart.Migrations
{
    /// <inheritdoc />
    public partial class RenameAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Students",
                newName: "AddressStreet");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Students",
                newName: "AddressState");

            migrationBuilder.RenameColumn(
                name: "ReferencePoint",
                table: "Students",
                newName: "AddressReferencePoint");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Students",
                newName: "AddressNumber");

            migrationBuilder.RenameColumn(
                name: "District",
                table: "Students",
                newName: "AddressDistrict");

            migrationBuilder.RenameColumn(
                name: "Complement",
                table: "Students",
                newName: "AddressComplement");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Students",
                newName: "AddressCity");

            migrationBuilder.RenameColumn(
                name: "CEP",
                table: "Students",
                newName: "AddressCEP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddressStreet",
                table: "Students",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "AddressState",
                table: "Students",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "AddressReferencePoint",
                table: "Students",
                newName: "ReferencePoint");

            migrationBuilder.RenameColumn(
                name: "AddressNumber",
                table: "Students",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "AddressDistrict",
                table: "Students",
                newName: "District");

            migrationBuilder.RenameColumn(
                name: "AddressComplement",
                table: "Students",
                newName: "Complement");

            migrationBuilder.RenameColumn(
                name: "AddressCity",
                table: "Students",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "AddressCEP",
                table: "Students",
                newName: "CEP");
        }
    }
}
