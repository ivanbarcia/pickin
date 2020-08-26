using Microsoft.EntityFrameworkCore.Migrations;

namespace Pickin.Migrations
{
    public partial class pedidos_precio3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Pedidos_Productos",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Pedidos_Productos");
        }
    }
}
