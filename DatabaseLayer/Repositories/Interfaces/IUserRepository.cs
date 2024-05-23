using DatabaseLayer.Entities;
using DatabaseLayer.Repositories.Base;

namespace DataBaseLayer.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public User? GetByName(string name);

    public void Add(User user);
}