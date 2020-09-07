using Microsoft.EntityFrameworkCore.Migrations;

namespace Pickin.Migrations
{
    public partial class empresa_celular2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Celular",
                table: "Empresas",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
