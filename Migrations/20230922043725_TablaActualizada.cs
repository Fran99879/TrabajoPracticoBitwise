using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabajoPracticoBit.Migrations
{
    /// <inheritdoc />
    public partial class TablaActualizada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Pedidos");

            migrationBuilder.AddColumn<string>(
                name: "NombreCliente",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreCliente",
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
