using DatabaseLayer.UnitOfWork;

namespace DatabaseLayer;

public class DbService : IDbService
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    public IUnitOfWork UnitOfWork => _unitOfWorkFactory.Create();
    
    public DbService(IUnitOfWorkFactory unitOfWorkFactory) => _unitOfWorkFactory = unitOfWorkFactory;
}