using DatabaseLayer.UnitOfWork;

namespace DatabaseLayer;

public class DbService : IDbService
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    public IUnitOfWork UnitOfWork => _unitOfWorkFactory.Create();

    public void ApplyMigrations() => UnitOfWork.ApplyMigrations();
    
    public DbService(IUnitOfWorkFactory unitOfWorkFactory) => _unitOfWorkFactory = unitOfWorkFactory;
}