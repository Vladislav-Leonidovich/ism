using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lpnu.Migrations.Lpnu
{
    /// <inheritdoc />
    public partial class ContactNamesUpd : Migration
    {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "StuffNameCollectionSerialized",
				table: "Contacts",
				type: "nvarchar(max)",
				nullable: false,
				oldClrType: typeof(List<string>),
				oldType: "nvarchar(max)");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<List<string>>(
				name: "StuffNameCollectionSerialized",
				table: "Contacts",
				type: "nvarchar(max)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)");
		}
	}
}
