using DatabaseLayer.Entities;
using DatabaseLayer.Repositories.Base;

namespace DataBaseLayer.Repositories.Interfaces;

public interface IChatRepository : IRepository<Chat>
{
    public List<Chat> GetQuestionsByUser(long userId);
    public List<Chat> GetAnswersByUser(long userId);
    public List<Chat> GetActiveQuestionsByUser(long userId);
    public List<Chat> GetActiveAnswersByUser(long userId);
}