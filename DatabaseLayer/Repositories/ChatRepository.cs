using DatabaseLayer.Entities;
using DatabaseLayer.Repositories.Base;
using DataBaseLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayer.Repositories;

public class ChatRepository : Repository<Chat>, IChatRepository
{
    public ChatRepository(DbContext context) : base(context)
    {
    }
}