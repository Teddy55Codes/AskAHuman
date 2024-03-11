using DatabaseLayer.Entities;
using DatabaseLayer.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayer.Repositories;

public class ChatRepository : Repository<Chat>
{
    public ChatRepository(DbContext context) : base(context)
    {
    }
}