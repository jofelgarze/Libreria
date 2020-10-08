﻿// <auto-generated />
using System;
using Libreria.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Libreria.Datos.Migrations
{
    [DbContext(typeof(LibreriaDbContext))]
    [Migration("20201008003406_DbInicial")]
    partial class DbInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Libreria.Datos.Entidades.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaRegistro");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("Libreria.Datos.Entidades.Libro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AutorId");

                    b.Property<decimal>("Precio");

                    b.Property<bool>("Publicado");

                    b.Property<string>("Titulo");

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.ToTable("Libros");
                });

            modelBuilder.Entity("Libreria.Datos.Entidades.Libro", b =>
                {
                    b.HasOne("Libreria.Datos.Entidades.Autor", "Autor")
                        .WithMany("Libros")
                        .HasForeignKey("AutorId");
                });
#pragma warning restore 612, 618
        }
    }
}
