using AskAHuman.Services.Interfaces;
using DatabaseLayer;
using DataBaseLayer.DTOs;
using DatabaseLayer.Entities;

namespace AskAHuman.Services;

public class ChatService : IChatService
{
    private readonly IDbService _dbService;
    
    public ChatService(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    public List<ChatCardDTO> GetAllChatsAsCards()
    {
        using var uow = _dbService.UnitOfWork;
        return uow.Chats.GetAll().Select(c => new ChatCardDTO(c.Id, c.Title, c.Question)).ToList();
    }

    public Chat CreateNewChat(long userId, string title, string question)
    {
        using var uow = _dbService.UnitOfWork;
        var newChat = new Chat
        {
            UsersAnswererId = null,
            UsersQuestioningId = userId,
            Title = title,
            Question = question
        };
        var chat = uow.Chats.Add(newChat);
        
        uow.Commit();
        return chat;
    }

    public Chat? GetChatById(long chatId)
    {
        using var uow = _dbService.UnitOfWork;
        return uow.Chats.GetByPrimaryKey(chatId);
    }
}