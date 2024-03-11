using DatabaseLayer.Entities;
using DatabaseLayer.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayer.Repositories;

public class UserRepository : Repository<User>
{
    public UserRepository(DbContext context) : base(context)
    {
    }
}