using DatabaseLayer.Entities;
using DatabaseLayer.Repositories.Base;

namespace DataBaseLayer.Repositories.Interfaces;

public interface IMessageRepository : IRepository<Message>
{
    public List<Message> GetMessagesByChatId(long chatId);
    
    public Message GetQuestionByChatId(long chatId);
}