using System.Linq.Expressions;

namespace DatabaseLayer.Repositories.Base;

public interface IRepository<TEntity> where TEntity : class
{
    public IEnumerable<TEntity> GetAll();
    public IEnumerable<TEntity> GetPage(int index, int pageSize);
    public TEntity? GetByPrimaryKey(object pk);
    public int GetCount();
    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    public IEnumerable<TEntity> FindPaged(Expression<Func<TEntity, bool>> predicate, int index, int size);
    
    public TEntity Add(TEntity entity);
    public void AddRange(IEnumerable<TEntity> entities);

    public TEntity Update(TEntity entity);

    public void RemoveByPrimaryKey(object pk);
    public void Remove(TEntity entity);
    public void RemoveRange(IEnumerable<TEntity> entities);
}