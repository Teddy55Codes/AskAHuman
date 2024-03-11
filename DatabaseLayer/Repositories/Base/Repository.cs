using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.Repositories.Base;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbContext _context;

    public Repository(DbContext context) => _context = context;

    public IEnumerable<TEntity> GetAll() => _context.Set<TEntity>().ToList();
    
    public IEnumerable<TEntity> GetPage(int index, int size) => _context.Set<TEntity>().Skip(index * size).Take(size).ToList(); 
    
    public TEntity? GetByPrimaryKey(object pk) => _context.Set<TEntity>().Find(pk);
    
    public int GetCount() => _context.Set<TEntity>().Count();
    
    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => _context.Set<TEntity>().Where(predicate).ToList();
    
    public IEnumerable<TEntity> FindPaged(Expression<Func<TEntity, bool>> predicate, int index, int size) => _context.Set<TEntity>().Where(predicate).Skip(index * size).Take(size).ToList();
    
    public TEntity Add(TEntity entity) => _context.Set<TEntity>().Add(entity).Entity;
    
    public void AddRange(IEnumerable<TEntity> entities) => _context.Set<TEntity>().AddRange(entities);
    
    public TEntity Update(TEntity entity) => _context.Set<TEntity>().Update(entity).Entity;
    
    public void RemoveByPrimaryKey(object pk)
    {
        var entity = GetByPrimaryKey(pk);
        if (entity == null) throw new KeyNotFoundException();
        _context.Set<TEntity>().Remove(entity);
    }

    public void Remove(TEntity entity) => _context.Set<TEntity>().Remove(entity);
    
    public void RemoveRange(IEnumerable<TEntity> entities) => _context.Set<TEntity>().RemoveRange(entities);
}