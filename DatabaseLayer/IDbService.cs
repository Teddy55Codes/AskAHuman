using DatabaseLayer.UnitOfWork;

namespace DatabaseLayer;

public interface IDbService
{
    public IUnitOfWork UnitOfWork { get; }
    public void ApplyMigrations();
}