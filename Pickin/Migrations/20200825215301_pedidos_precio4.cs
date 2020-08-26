using Microsoft.EntityFrameworkCore.Migrations;

namespace Pickin.Migrations
{
    public partial class pedidos_precio4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "Pedidos",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Depto",
                table: "Pedidos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Pedidos",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DireccionNro",
                table: "Pedidos",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Entrecalles",
                table: "Pedidos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Pedidos",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Piso",
                table: "Pedidos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Depto",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "DireccionNro",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Entrecalles",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Piso",
                table: "Pedidos");
        }
    }
}
