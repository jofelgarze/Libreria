using Microsoft.EntityFrameworkCore.Migrations;

namespace Libreria.Datos.Migrations
{
    public partial class ScriptCorreccionDatosAutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Script de correccion X
            migrationBuilder.Sql("INSERT INTO [dbo].[Libros] ([Titulo],[AutorId],[Publicado],[Precio]) VALUES ('Titulo 1', 1, 0, 15.65)");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //Script de reverso Y
            migrationBuilder.Sql("select * Autores;");
        }
    }
}
