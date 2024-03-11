using DatabaseLayer.UnitOfWork;
using DatabaseLayer.Context;
using DataBaseLayer.Repositories;

namespace DatabaseLayer.UnitOfWork;

/// <summary>
/// implementation of "unit of work" pattern
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AskAHumanDbContext _context;
    
    public UnitOfWork(AskAHumanDbContext context) => _context = context;
    
    private ChatRepository? _chatRepository;
    public ChatRepository Chats => _chatRepository ??= new ChatRepository(_context);
    
    private MessageRepository? _messageRepository;
    public MessageRepository Messages => _messageRepository ??= new MessageRepository(_context);
        
    private PermissionRepository? _permissionRepository;
    public PermissionRepository Permissions => _permissionRepository ??= new PermissionRepository(_context);
            
    private RoleRepository? _roleRepository;
    public RoleRepository Roles => _roleRepository ??= new RoleRepository(_context);
    
    private UserRepository? _userRepository;
    public UserRepository Users => _userRepository ??= new UserRepository(_context);
    
    public void Commit() => _context.SaveChanges();

    public void Dispose() => _context.Dispose();
}