using DatabaseLayer.Entities;
using DatabaseLayer.Entities.Enums;
using DatabaseLayer.Repositories.Base;
using DataBaseLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayer.Repositories;

public class ChatRepository : Repository<Chat>, IChatRepository
{
    public ChatRepository(DbContext context) : base(context)
    {
    }

    public List<Chat> GetChatsRelatedToUser(long userId) => _context.Set<Chat>().Include(c => c.UsersQuestioning).Where(c => c.UsersAnswererId == userId || c.UsersQuestioningId == userId).ToList();
    public List<Chat> GetUnansweredChats() => _context.Set<Chat>().Include(c => c.UsersQuestioning).Where(c => c.UsersAnswererId == null && c.State == ChatState.Open).ToList();
    public List<Chat> GetQuestionsByUser(long userId) => _context.Set<Chat>().Include(c => c.UsersQuestioning).Where(c => c.UsersQuestioningId == userId).ToList();
    public List<Chat> GetAnswersByUser(long userId) => _context.Set<Chat>().Include(c => c.UsersQuestioning).Where(c => c.UsersAnswererId == userId).ToList();
    public List<Chat> GetActiveQuestionsByUser(long userId) => _context.Set<Chat>().Include(c => c.UsersQuestioning).Where(c => c.State == ChatState.Open && c.UsersQuestioningId == userId).ToList();
    public List<Chat> GetActiveAnswersByUser(long userId) => _context.Set<Chat>().Include(c => c.UsersQuestioning).Where(c => c.State == ChatState.Open && c.UsersAnswererId == userId).ToList();

}