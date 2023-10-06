﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistencia.Data;

#nullable disable

namespace Persistencia.Data.Migrations
{
    [DbContext(typeof(DbAppContext))]
    [Migration("20231005233048_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Dominio.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("RefreshToken", (string)null);
                });

            modelBuilder.Entity("Dominio.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IdRol")
                        .HasAnnotation("MySqlValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("NombreRol");

                    b.HasKey("Id");

                    b.ToTable("Rol", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Administrador"
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Gerente"
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Empleado"
                        },
                        new
                        {
                            Id = 4,
                            Nombre = "Persona"
                        });
                });

            modelBuilder.Entity("Dominio.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id_Usuario")
                        .HasAnnotation("MySqlValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("Email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("Password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Usuario", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "britodelgado514@gmail.com",
                            Password = "1234",
                            Username = "Sicer Brito"
                        },
                        new
                        {
                            Id = 2,
                            Email = "angedeveloper@gmail.com",
                            Password = "12345",
                            Username = "Angelica Morales"
                        },
                        new
                        {
                            Id = 3,
                            Email = "lisethtorres969@gmail.com",
                            Password = "123456",
                            Username = "Konny Alucemna"
                        });
                });

            modelBuilder.Entity("Dominio.Entities.UsuarioRoles", b =>
                {
                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId", "RolId");

                    b.HasIndex("RolId");

                    b.ToTable("UsuariosRoles");
                });

            modelBuilder.Entity("Dominio.Entities.RefreshToken", b =>
                {
                    b.HasOne("Dominio.Entities.Usuario", "Usuario")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Dominio.Entities.UsuarioRoles", b =>
                {
                    b.HasOne("Dominio.Entities.Rol", "Roles")
                        .WithMany("UsuarioRoles")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entities.Usuario", "Usuarios")
                        .WithMany("UsuarioRoles")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Dominio.Entities.Rol", b =>
                {
                    b.Navigation("UsuarioRoles");
                });

            modelBuilder.Entity("Dominio.Entities.Usuario", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("UsuarioRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
