using Microsoft.EntityFrameworkCore.Migrations;

namespace Pickin.Migrations
{
    public partial class pedidos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Pedidos_Productos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CantidadTotal",
                table: "Pedidos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MontoTotal",
                table: "Pedidos",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Pedidos_Productos");

            migrationBuilder.DropColumn(
                name: "CantidadTotal",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "MontoTotal",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Pedidos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
