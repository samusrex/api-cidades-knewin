using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Populacao = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fronteiras",
                columns: table => new
                {
                    CidadesId1 = table.Column<int>(type: "INTEGER", nullable: false),
                    CidadesId2 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fronteiras", x => new { x.CidadesId1, x.CidadesId2 });
                    table.ForeignKey(
                        name: "FK_Fronteiras_Cidades_CidadesId1",
                        column: x => x.CidadesId1,
                        principalTable: "Cidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fronteiras_Cidades_CidadesId2",
                        column: x => x.CidadesId2,
                        principalTable: "Cidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fronteiras_CidadesId2",
                table: "Fronteiras",
                column: "CidadesId2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fronteiras");

            migrationBuilder.DropTable(
                name: "Cidades");
        }
    }
}
