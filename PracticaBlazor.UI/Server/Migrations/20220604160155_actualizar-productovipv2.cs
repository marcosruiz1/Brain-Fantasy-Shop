using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticaBlazor.UI.Server.Migrations
{
    public partial class actualizarproductovipv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "ProductoVIP",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "ProductoVIP");
        }
    }
}
