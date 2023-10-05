using System.Linq.Expressions;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Aplicacion.Repository;
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity{

        private readonly DbAppContext _Context;

        public GenericRepository(DbAppContext context)
        {
            _Context = context;
        }

        public virtual void Add(T entity)
        {
            _Context.Set<T>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _Context.Set<T>().AddRange(entities);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _Context.Set<T>().Where(expression);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _Context.Set<T>().ToListAsync();
            
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return (await _Context.Set<T>().FindAsync(id))!;
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
        return (await _Context.Set<T>().FindAsync(id))!;
        }

        public virtual void Remove(T entity)
        {
            _Context.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _Context.Set<T>().RemoveRange(entities);
        }

        public virtual void Update(T entity)
        {
            _Context.Set<T>()
                .Update(entity);
        }
        public virtual async Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
        {
            var totalRegistros = await _Context.Set<T>().CountAsync();
            var registros = await _Context.Set<T>()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (totalRegistros, registros);
        }
        
    }
