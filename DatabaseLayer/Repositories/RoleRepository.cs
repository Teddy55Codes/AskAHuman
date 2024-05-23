using DatabaseLayer.Entities;
using DatabaseLayer.Repositories.Base;
using DataBaseLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayer.Repositories;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(DbContext context) : base(context)
    {
    }
}