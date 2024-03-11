namespace DatabaseLayer.Context;

public interface IAskAHumanDbContextFactory
{
    public AskAHumanDbContext Create();
}