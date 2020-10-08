using Microsoft.EntityFrameworkCore.Migrations;

namespace Libreria.Datos.Migrations
{
    public partial class TblAlterAutores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Autores",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Autores");
        }
    }
}
