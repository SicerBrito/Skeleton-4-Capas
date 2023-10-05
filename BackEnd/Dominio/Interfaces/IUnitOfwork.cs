namespace Dominio.Interfaces;
    public interface IUnitOfWork{
        
        IUsuario ? Usuarios { get; }
        IRol ? Roles { get; }
        IUsuarioRoles UsuariosRoles { get; }
        Task<int> SaveAsync();
        
    }
