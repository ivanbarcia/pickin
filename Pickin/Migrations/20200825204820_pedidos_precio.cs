using Microsoft.EntityFrameworkCore.Migrations;

namespace Pickin.Migrations
{
    public partial class pedidos_precio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Precio",
                table: "Pedidos_Productos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Pedidos_Productos");
        }
    }
}
