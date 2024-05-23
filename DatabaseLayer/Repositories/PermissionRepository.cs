using DatabaseLayer.Entities;
using DatabaseLayer.Repositories.Base;
using DataBaseLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayer.Repositories;

public class PermissionRepository : Repository<Permission>, IPermissionRepository
{
    public PermissionRepository(DbContext context) : base(context)
    {
    }
}