using DatabaseLayer.Entities;
using DatabaseLayer.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayer.Repositories;

public class RoleRepository : Repository<Role>
{
    public RoleRepository(DbContext context) : base(context)
    {
    }
}