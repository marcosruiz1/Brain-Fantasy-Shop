using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticaBlazor.UI.Server.Migrations
{
    public partial class actualizarproductovip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductoVIP_Usuario_UsuarioId",
                table: "ProductoVIP");

            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "ProductoVIP",
                newName: "PrecioMin");

            migrationBuilder.RenameColumn(
                name: "Categoria",
                table: "ProductoVIP",
                newName: "PrecioMax");

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "ProductoVIP",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "ProductoVIP",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoVIP_Usuario_UsuarioId",
                table: "ProductoVIP",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductoVIP_Usuario_UsuarioId",
                table: "ProductoVIP");

            migrationBuilder.RenameColumn(
                name: "PrecioMin",
                table: "ProductoVIP",
                newName: "Precio");

            migrationBuilder.RenameColumn(
                name: "PrecioMax",
                table: "ProductoVIP",
                newName: "Categoria");

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "ProductoVIP",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "ProductoVIP",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoVIP_Usuario_UsuarioId",
                table: "ProductoVIP",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
