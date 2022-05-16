using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticaBlazor.UI.Server.Migrations
{
    public partial class carritoproducto3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrito_Producto_ProductoId",
                table: "Carrito");

            migrationBuilder.DropIndex(
                name: "IX_Carrito_ProductoId",
                table: "Carrito");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Carrito");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Carrito",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carrito_ProductoId",
                table: "Carrito",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrito_Producto_ProductoId",
                table: "Carrito",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id");
        }
    }
}
