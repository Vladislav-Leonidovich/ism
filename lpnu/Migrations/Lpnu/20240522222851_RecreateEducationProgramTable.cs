using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lpnu.Migrations.Lpnu
{
    /// <inheritdoc />
    public partial class RecreateEducationProgramTable : Migration
    {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "EducationPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Level = table.Column<int>(type: "int", nullable: false)
				},
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationPrograms", x => x.Id);
                });
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "EducationPrograms");
		}
	}
}
