using DatabaseLayer.Context;
using DataBaseLayer.Repositories;
using DataBaseLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer.UnitOfWork;

/// <summary>
/// implementation of "unit of work" pattern
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AskAHumanDbContext _context;
    
    public UnitOfWork(AskAHumanDbContext context) => _context = context;
    
    private ChatRepository? _chatRepository;
    public IChatRepository Chats => _chatRepository ??= new ChatRepository(_context);
    
    private MessageRepository? _messageRepository;
    public IMessageRepository Messages => _messageRepository ??= new MessageRepository(_context);
        
    private PermissionRepository? _permissionRepository;
    public IPermissionRepository Permissions => _permissionRepository ??= new PermissionRepository(_context);
            
    private RoleRepository? _roleRepository;
    public IRoleRepository Roles => _roleRepository ??= new RoleRepository(_context);
    
    private UserRepository? _userRepository;
    public IUserRepository Users => _userRepository ??= new UserRepository(_context);
    
    public void Commit() => _context.SaveChanges();

    public void ApplyMigrations() => _context.Database.Migrate();

    public void Dispose() => _context.Dispose();
}