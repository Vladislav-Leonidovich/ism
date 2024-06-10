using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lpnu.Migrations.Lpnu
{
    /// <inheritdoc />
    public partial class ChangeStuffNameCollectionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StuffNameCollection",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "StuffNameCollectionSerialized",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StuffNameCollectionSerialized",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "StuffNameCollection",
                table: "Contacts",
                nullable: true);
        }
    }
}
