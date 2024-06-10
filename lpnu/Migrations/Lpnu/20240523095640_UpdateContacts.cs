using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lpnu.Migrations.Lpnu
{
    /// <inheritdoc />
    public partial class UpdateContacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Employees",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "Teachers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_ContactId",
                table: "Teachers",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Contacts_ContactId",
                table: "Teachers",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Contacts_ContactId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_ContactId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Teachers");

            migrationBuilder.AddColumn<string>(
                name: "Employees",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
