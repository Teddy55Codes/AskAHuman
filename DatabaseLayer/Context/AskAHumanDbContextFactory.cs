using Microsoft.Extensions.Configuration;

namespace DatabaseLayer.Context;

public class AskAHumanDbContextFactory : IAskAHumanDbContextFactory
{
    private IConfiguration _configuration;

    public AskAHumanDbContextFactory(IConfiguration configuration) => _configuration = configuration;
    
    public AskAHumanDbContext Create() => new(_configuration);
    
}