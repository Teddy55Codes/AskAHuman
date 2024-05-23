using DatabaseLayer.Entities;
using DatabaseLayer.Repositories.Base;
using DataBaseLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayer.Repositories;

public class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(DbContext context) : base(context)
    {
    }

    public List<Message> GetMessagesByChatId(long chatId) => _context.Set<Message>().Where(m => m.ChatId == chatId).ToList();
    
    public Message GetQuestionByChatId(long chatId) => _context.Set<Message>().First(m => m.ChatId == chatId);

}