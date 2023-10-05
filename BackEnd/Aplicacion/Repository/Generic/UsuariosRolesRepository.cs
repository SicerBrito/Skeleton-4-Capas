
using System.Linq.Expressions;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using Persistencia.Data;

namespace Aplicacion.Repository;
public class UsuariosRolesRepository : IUsuarioRoles
{
    private readonly DbAppContext _Context;

    public UsuariosRolesRepository(DbAppContext context)
    {
        _Context = context;
    }

    //implementacion de los metodos de la Interfaces
    public void Add(UsuarioRoles entity)
    {
        _Context.Set<UsuarioRoles>().Add(entity);
    }

    public void AddRange(IEnumerable<UsuarioRoles> entities)
    {
        _Context.Set<UsuarioRoles>().AddRange(entities);
    }

    public IEnumerable<UsuarioRoles> Find(Expression<Func<UsuarioRoles, bool>> expression)
    {
        return _Context.Set<UsuarioRoles>().Where(expression);
    }

    public async Task<IEnumerable<UsuarioRoles>> GetAllAsync()
    {
        return await _Context.Set<UsuarioRoles>().ToListAsync();
    }

    public async Task<(int totalRegistros, IEnumerable<UsuarioRoles> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var totalRegistros = await _Context.Set<UsuarioRoles>().CountAsync();
        var registros = await _Context.Set<UsuarioRoles>()
                                                        .Skip((pageIndex - 1) * pageSize)
                                                        .Take(pageSize)
                                                        .ToListAsync();

        return (totalRegistros, registros);
    }

    public async Task<UsuarioRoles> GetByIdAsync(int idUsuario, int idRol)
    {
        return (await _Context.Set<UsuarioRoles>().FirstOrDefaultAsync(p => (p.UsuarioId == idUsuario && p.RolId == idRol)))!;
    }

    public void Remove(UsuarioRoles entity)
    {
        _Context.Set<UsuarioRoles>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<UsuarioRoles> entities)
    {
        _Context.Set<UsuarioRoles>().RemoveRange(entities);
    }

    public void Update(UsuarioRoles entity)
    {
        _Context.Set<UsuarioRoles>().Update(entity);
    }
}
