using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Libreria.Datos.Migrations
{
    public partial class TblAutor_AddFotoPerfil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FotoPerfil",
                table: "Autores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoPerfil",
                table: "Autores");
        }
    }
}
