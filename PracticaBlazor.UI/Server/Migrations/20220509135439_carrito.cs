using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticaBlazor.UI.Server.Migrations
{
    public partial class carrito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carrito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idProducto = table.Column<int>(type: "int", nullable: false),
                    numProductos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrito", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carrito");
        }
    }
}
