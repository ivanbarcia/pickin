using Microsoft.EntityFrameworkCore.Migrations;

namespace Pickin.Migrations
{
    public partial class empresa_celular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Pedidos",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
