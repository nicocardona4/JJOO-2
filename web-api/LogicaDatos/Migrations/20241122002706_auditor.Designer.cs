﻿// <auto-generated />
using System;
using LogicaDatos.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LogicaDatos.Migrations
{
    [DbContext(typeof(OlimpiadasContext))]
    [Migration("20241122002706_auditor")]
    partial class auditor
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AtletaDisciplina", b =>
                {
                    b.Property<int>("AtletaId")
                        .HasColumnType("int");

                    b.Property<int>("DisciplinaId")
                        .HasColumnType("int");

                    b.HasKey("AtletaId", "DisciplinaId");

                    b.HasIndex("DisciplinaId");

                    b.ToTable("AtletaDisciplina");
                });

            modelBuilder.Entity("AtletaEvento", b =>
                {
                    b.Property<int>("AtletasId")
                        .HasColumnType("int");

                    b.Property<string>("_eventosNombrePrueba")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AtletasId", "_eventosNombrePrueba");

                    b.HasIndex("_eventosNombrePrueba");

                    b.ToTable("AtletaEvento");
                });

            modelBuilder.Entity("LogicaNegocio.EntidadesDominio.Atleta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaisNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PaisNombre");

                    b.ToTable("Atletas");
                });

            modelBuilder.Entity("LogicaNegocio.EntidadesDominio.Auditoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Entidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntidadId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaOperacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Operacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Auditorias");
                });

            modelBuilder.Entity("LogicaNegocio.EntidadesDominio.Disciplina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Disciplinas");
                });

            modelBuilder.Entity("LogicaNegocio.EntidadesDominio.Evento", b =>
                {
                    b.Property<string>("NombrePrueba")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DisciplinaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicial")
                        .HasColumnType("datetime2");

                    b.HasKey("NombrePrueba");

                    b.HasIndex("DisciplinaId");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("LogicaNegocio.EntidadesDominio.Pais", b =>
                {
                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("cantHabitantes")
                        .HasColumnType("int");

                    b.Property<int>("telDelegado")
                        .HasColumnType("int");

                    b.HasKey("Nombre");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("LogicaNegocio.EntidadesDominio.Participaciones", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AtletaId")
                        .HasColumnType("int");

                    b.Property<string>("EventoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Puntuacion")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AtletaId");

                    b.HasIndex("EventoId");

                    b.ToTable("Participaciones");
                });

            modelBuilder.Entity("LogicaNegocio.EntidadesDominio.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("LogicaNegocio.EntidadesDominio.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Admin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("AtletaDisciplina", b =>
                {
                    b.HasOne("LogicaNegocio.EntidadesDominio.Atleta", null)
                        .WithMany()
                        .HasForeignKey("AtletaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.EntidadesDominio.Disciplina", null)
                        .WithMany()
                        .HasForeignKey("DisciplinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AtletaEvento", b =>
                {
                    b.HasOne("LogicaNegocio.EntidadesDominio.Atleta", null)
                        .WithMany()
                        .HasForeignKey("AtletasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.EntidadesDominio.Evento", null)
                        .WithMany()
                        .HasForeignKey("_eventosNombrePrueba")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LogicaNegocio.EntidadesDominio.Atleta", b =>
                {
                    b.HasOne("LogicaNegocio.EntidadesDominio.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisNombre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pais");
                });

            modelBuilder.Entity("LogicaNegocio.EntidadesDominio.Disciplina", b =>
                {
                    b.OwnsOne("LogicaNegocio.ValueObjects.Anio", "Anio", b1 =>
                        {
                            b1.Property<int>("DisciplinaId")
                                .HasColumnType("int");

                            b1.Property<int>("Valor")
                                .HasColumnType("int");

                            b1.HasKey("DisciplinaId");

                            b1.ToTable("Disciplinas");

                            b1.WithOwner()
                                .HasForeignKey("DisciplinaId");
                        });

                    b.OwnsOne("LogicaNegocio.ValueObjects.Codigo", "Codigo", b1 =>
                        {
                            b1.Property<int>("DisciplinaId")
                                .HasColumnType("int");

                            b1.Property<int>("Valor")
                                .HasColumnType("int");

                            b1.HasKey("DisciplinaId");

                            b1.ToTable("Disciplinas");

                            b1.WithOwner()
                                .HasForeignKey("DisciplinaId");
                        });

                    b.OwnsOne("LogicaNegocio.ValueObjects.NombreDisciplina", "NombreDisciplina", b1 =>
                        {
                            b1.Property<int>("DisciplinaId")
                                .HasColumnType("int");

                            b1.Property<string>("Valor")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("DisciplinaId");

                            b1.ToTable("Disciplinas");

                            b1.WithOwner()
                                .HasForeignKey("DisciplinaId");
                        });

                    b.Navigation("Anio")
                        .IsRequired();

                    b.Navigation("Codigo")
                        .IsRequired();

                    b.Navigation("NombreDisciplina")
                        .IsRequired();
                });

            modelBuilder.Entity("LogicaNegocio.EntidadesDominio.Evento", b =>
                {
                    b.HasOne("LogicaNegocio.EntidadesDominio.Disciplina", "Disciplina")
                        .WithMany()
                        .HasForeignKey("DisciplinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Disciplina");
                });

            modelBuilder.Entity("LogicaNegocio.EntidadesDominio.Participaciones", b =>
                {
                    b.HasOne("LogicaNegocio.EntidadesDominio.Atleta", "Atleta")
                        .WithMany()
                        .HasForeignKey("AtletaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogicaNegocio.EntidadesDominio.Evento", "Evento")
                        .WithMany("Participaciones")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atleta");

                    b.Navigation("Evento");
                });

            modelBuilder.Entity("LogicaNegocio.EntidadesDominio.Usuario", b =>
                {
                    b.HasOne("LogicaNegocio.EntidadesDominio.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("LogicaNegocio.ValueObject.Contrasenia", "Contrasenia", b1 =>
                        {
                            b1.Property<int>("UsuarioId")
                                .HasColumnType("int");

                            b1.Property<string>("Valor")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuarios");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.OwnsOne("LogicaNegocio.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<int>("UsuarioId")
                                .HasColumnType("int");

                            b1.Property<string>("Valor")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuarios");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.Navigation("Contrasenia")
                        .IsRequired();

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("LogicaNegocio.EntidadesDominio.Evento", b =>
                {
                    b.Navigation("Participaciones");
                });
#pragma warning restore 612, 618
        }
    }
}
