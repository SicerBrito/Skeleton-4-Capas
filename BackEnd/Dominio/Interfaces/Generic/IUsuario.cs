using Dominio.Entities;

namespace Dominio.Interfaces;
    public interface IUsuario : IGenericRepository<Usuario>{

        Task<Usuario> GetByUsernameAsync(string username);
        Task<Usuario> GetByRefreshTokenAsync(string username);
        
    }
