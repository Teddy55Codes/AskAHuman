using DatabaseLayer.Context;

namespace DatabaseLayer.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IAskAHumanDbContextFactory _askAHumanContextFactory;

    public UnitOfWorkFactory(IAskAHumanDbContextFactory askAHumanContextFactory) => _askAHumanContextFactory = askAHumanContextFactory;
    
    public IUnitOfWork Create() => new UnitOfWork(_askAHumanContextFactory.Create());
}