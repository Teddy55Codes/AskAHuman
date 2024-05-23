using DataBaseLayer.Repositories;
using DataBaseLayer.Repositories.Interfaces;

namespace DatabaseLayer.UnitOfWork;

public interface IUnitOfWork : IDisposable
{ 
    public IChatRepository Chats { get; }
    public IMessageRepository Messages { get; }
    public IPermissionRepository Permissions { get; }
    public IRoleRepository Roles { get; }
    public IUserRepository Users { get; }
    
    public void Commit();
    public void ApplyMigrations();
}