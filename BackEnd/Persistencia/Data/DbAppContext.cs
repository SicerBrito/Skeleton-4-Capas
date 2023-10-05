using System.Reflection;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia.Data;
    public class DbAppContext : DbContext{

        public DbAppContext(DbContextOptions<DbAppContext> options) : base (options){

        }

        public DbSet<Usuario> ? Usuarios { get; set; } = null!;
        public DbSet<Rol> ? Roles { get; set; } = null!;
        public DbSet<UsuarioRoles> ? UsuariosRoles { get; set; } = null!;
        public DbSet<RefreshToken> ? RefreshTokens { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
    }
