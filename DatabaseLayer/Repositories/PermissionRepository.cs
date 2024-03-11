using DatabaseLayer.Entities;
using DatabaseLayer.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayer.Repositories;

public class PermissionRepository : Repository<Permission>
{
    public PermissionRepository(DbContext context) : base(context)
    {
    }
}