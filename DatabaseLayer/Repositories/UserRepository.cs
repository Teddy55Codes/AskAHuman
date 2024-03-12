using DatabaseLayer.Entities;
using DatabaseLayer.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayer.Repositories;

public class UserRepository : Repository<User>
{
    public UserRepository(DbContext context) : base(context)
    {
    }
    
    public User? GetByName(string name) => _context.Set<User>().FirstOrDefault(u => string.Equals(u.Username, name));
        
    public void Add(User user)
    {
        user.Username = user.Username.ToLower(); 
        _context.Set<User>().Add(user);
    }
}