using AskAHuman.Services.Interfaces;
using DatabaseLayer;
using DataBaseLayer.DTOs;
using DatabaseLayer.Entities;
using DatabaseLayer.Entities.Enums;
using FluentResults;

namespace AskAHuman.Services;

public class ChatService : IChatService
{
    private readonly IConfiguration _configuration;
    private readonly IDbService _dbService;
    
    public ChatService(IConfiguration configuration, IDbService dbService)
    {
        _configuration = configuration;
        _dbService = dbService;
    }
    
    /// <inheritdoc />
    public List<ChatCardDTO> GetAllChatsAsCards()
    {
        using var uow = _dbService.UnitOfWork;
        return uow.Chats.GetAll().Select(c => new ChatCardDTO(c.Id, c.Title, c.Question)).ToList();
    }

    /// <inheritdoc />
    public List<ChatCardDTO> GetUnansweredChatsAsCards()
    {
        using var uow = _dbService.UnitOfWork;
        return uow.Chats.GetUnansweredChats().Select(c => new ChatCardDTO(c.Id, c.Title, c.Question)).ToList();
    }

    /// <inheritdoc />
    public List<ChatCardDTO> GetUsersActiveChatsAsCards(long userId)
    {
        using var uow = _dbService.UnitOfWork;
        return uow.Chats.GetChatsRelatedToUser(userId).Where(c => c.State == ChatState.Open).Select(c => new ChatCardDTO(c.Id, c.Title, c.Question)).ToList();
    }

    /// <inheritdoc />
    public List<ChatCardDTO> GetUsersCompletedChatsAsCards(long userId)
    {
        using var uow = _dbService.UnitOfWork;
        return uow.Chats.GetChatsRelatedToUser(userId).Where(c => c.State != ChatState.Open).Select(c => new ChatCardDTO(c.Id, c.Title, c.Question)).ToList();
    }

    /// <inheritdoc />
    public Result<Chat> CreateNewChat(long userId, string title, string question)
    {
        using var unitOfWork = _dbService.UnitOfWork;
        var user = unitOfWork.Users.GetByPrimaryKey(userId);
        if (user is null) return Result.Fail("User does not exist.");
        
        if (int.TryParse(_configuration["Administration:MaxActiveQuestions"], out var maxActiveQuestions) &&
            user.ChatUsersQuestionings.Count >= maxActiveQuestions)
        {
            return Result.Fail($"Maximum active questions reached. You can only have {maxActiveQuestions} active questions.");
        }
        
        var newChat = new Chat
        {
            UsersAnswererId = null,
            UsersQuestioningId = userId,
            Title = title,
            Question = question
        };
        var chat = unitOfWork.Chats.Add(newChat);
        
        unitOfWork.Commit();
        return chat;
    }

    /// <inheritdoc />
    public Result<Chat> ClaimChat(long userId, long chatId)
    {
        using var unitOfWork = _dbService.UnitOfWork;
        var user = unitOfWork.Users.GetByPrimaryKey(userId);
        if (user is null) return Result.Fail("User does not exist.");
        
        if (int.TryParse(_configuration["Administration:MaxActiveAnswers"], out var maxActiveAnswers) &&
            user.ChatUsersAnswerers.Count >= maxActiveAnswers)
        {
            return Result.Fail($"Maximum active answers reached. You can only have {maxActiveAnswers} active answers.");
        }
        
        var chat = unitOfWork.Chats.GetByPrimaryKey(chatId);
        if (chat is null) return Result.Fail("Question does not exist.");
        if (chat.UsersAnswererId is not null) return Result.Fail("Question already has an answerer.");
        
        chat.UsersAnswererId = userId;
        chat.AnswererJoinedAt = DateTime.Now.ToUniversalTime();
        unitOfWork.Commit();
        return chat;

    }

    /// <inheritdoc />
    public Result<Chat> RemoveAnswererFromChat(long chatId)
    {
        using var unitOfWork = _dbService.UnitOfWork;
        var chat = unitOfWork.Chats.GetByPrimaryKey(chatId);
        if (chat is null) return Result.Fail("Question does not exist.");
        if (double.TryParse(_configuration["Administration:QuestionLeavableTimeInHours"], out var leaveableTimer) &&
            DateTime.Now.ToUniversalTime() - chat.AnswererJoinedAt <= TimeSpan.FromHours(leaveableTimer))
        {
            return Result.Fail($"You can only leave a question after {leaveableTimer} hours.");
        }

        chat.UsersAnswerer = null;
        chat.UsersAnswererId = null;
        unitOfWork.Commit();
        return chat;

    }

    /// <inheritdoc />
    public Chat? GetChatById(long chatId)
    {
        using var uow = _dbService.UnitOfWork;
        return uow.Chats.GetByPrimaryKey(chatId);
    }
}