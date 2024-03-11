using DatabaseLayer.Entities;
using DatabaseLayer.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayer.Repositories;

public class MessageRepository : Repository<Message>
{
    public MessageRepository(DbContext context) : base(context)
    {
    }
}