using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.Repository;
public class RolRepository : GenericRepository<Rol>, IRol
{
    private readonly DbAppContext _Context;
    public RolRepository(DbAppContext context) : base(context)
    {
        _Context = context;
    }
}
