namespace DatabaseLayer.UnitOfWork;

public interface IUnitOfWorkFactory
{
    public IUnitOfWork Create();
}