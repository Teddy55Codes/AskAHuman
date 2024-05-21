using System.Linq.Expressions;

namespace DatabaseLayer.Repositories.Base;

public interface IRepository<TEntity> where TEntity : class
{
    public List<TEntity> GetAll();
    public List<TEntity> GetPage(int index, int pageSize);
    public TEntity? GetByPrimaryKey(object pk);
    public int GetCount();
    public List<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    public List<TEntity> FindPaged(Expression<Func<TEntity, bool>> predicate, int index, int size);
    
    public TEntity Add(TEntity entity);
    public void AddRange(IEnumerable<TEntity> entities);

    public TEntity Update(TEntity entity);

    public void RemoveByPrimaryKey(object pk);
    public void Remove(TEntity entity);
    public void RemoveRange(IEnumerable<TEntity> entities);
}