using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promart.Migrations
{
    /// <inheritdoc />
    public partial class RenameStudentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyRelationshipStudent_FamilyRelationships_RelationshipsId",
                table: "FamilyRelationshipStudent");

            migrationBuilder.RenameColumn(
                name: "RelationshipsId",
                table: "FamilyRelationshipStudent",
                newName: "FamilyRelationshipsId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyRelationshipStudent_FamilyRelationships_FamilyRelationshipsId",
                table: "FamilyRelationshipStudent",
                column: "FamilyRelationshipsId",
                principalTable: "FamilyRelationships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyRelationshipStudent_FamilyRelationships_FamilyRelationshipsId",
                table: "FamilyRelationshipStudent");

            migrationBuilder.RenameColumn(
                name: "FamilyRelationshipsId",
                table: "FamilyRelationshipStudent",
                newName: "RelationshipsId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyRelationshipStudent_FamilyRelationships_RelationshipsId",
                table: "FamilyRelationshipStudent",
                column: "RelationshipsId",
                principalTable: "FamilyRelationships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
