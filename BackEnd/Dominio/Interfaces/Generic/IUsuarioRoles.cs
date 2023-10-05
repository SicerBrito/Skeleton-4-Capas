using System.Linq.Expressions;
using Dominio.Entities;

namespace Dominio.Interfaces;
public interface IUsuarioRoles{

    Task<UsuarioRoles> GetByIdAsync(int idUsua, int idRol);
    Task<IEnumerable<UsuarioRoles>> GetAllAsync();
    IEnumerable<UsuarioRoles> Find(Expression<Func<UsuarioRoles, bool>> expression);
    Task<(int totalRegistros, IEnumerable<UsuarioRoles> registros)> GetAllAsync(int pageIndex, int pageSize, string search);
    void Add(UsuarioRoles entity);
    void AddRange(IEnumerable<UsuarioRoles> entities);
    void Remove(UsuarioRoles entity);
    void RemoveRange(IEnumerable<UsuarioRoles> entities);
    void Update(UsuarioRoles entity);
      
}