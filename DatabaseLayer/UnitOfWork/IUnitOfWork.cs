using DataBaseLayer.Repositories;

namespace DatabaseLayer.UnitOfWork;

public interface IUnitOfWork : IDisposable
{ 
    public ChatRepository Chats { get; }
    public MessageRepository Messages { get; }
    public PermissionRepository Permissions { get; }
    public RoleRepository Roles { get; }
    public UserRepository Users { get; }
    
    public void Commit();
}