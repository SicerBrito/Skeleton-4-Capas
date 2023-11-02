using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuracion;
    public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.Property(p => p.Id)
                .HasAnnotation("MySqlValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasColumnName("Id_Usuario")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Username)
                .HasColumnName("Username")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnName("Email")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(p => p.Username)
            .IsUnique();

            builder.HasIndex(p => p.Email)
            .IsUnique();

            builder.Property(p => p.Password)
                .HasColumnName("Password")
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();

            builder.HasMany(p => p.RefreshTokens)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.UsuarioId);

            //se define la configuracion de la entidad UsuariosRoles
            builder
            .HasMany(p => p.Roles)
            .WithMany(p => p.Usuarios)
            .UsingEntity<UsuarioRoles> (
                j => j
                    .HasOne(p => p.Roles)
                    .WithMany(p => p.UsuarioRoles)
                    .HasForeignKey(p => p.RolId),

                j => j
                    .HasOne(p => p.Usuarios)
                    .WithMany(p => p.UsuarioRoles)
                    .HasForeignKey(p => p.UsuarioId),

                j => 
                    {
                        j.HasKey(p => new { p.UsuarioId, p.RolId});
                    }
            );

            builder.HasData(
                new {
                    Id = 1,
                    Username = "Sicer Brito",
                    Email = "britodelgado514@gmail.com",
                    Password = "1234"
                },
                new {
                    Id = 2,
                    Username = "Angelica Morales",
                    Email = "angedeveloper@gmail.com",
                    Password = "12345"
                },
                new {
                    Id = 3,
                    Username = "Konny Alucemna",
                    Email = "lisethtorres969@gmail.com",
                    Password = "123456"
                }
            );
        
        }
    }