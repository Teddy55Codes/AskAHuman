using AskAHuman.Services.Interfaces;
using DatabaseLayer;
using DataBaseLayer.DTOs;

namespace AskAHuman.Services;

public class MessageService : IMessageService
{
    private readonly IDbService _dbService;
    
    public MessageService(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    /// <inheritdoc />
    public List<MessageDTO> GetMessagesForChatById(long chatId)
    {
        using var uow = _dbService.UnitOfWork;
        return uow.Messages.GetMessagesByChatId(chatId).Select(m => new MessageDTO(m.Content, uow.Users.GetByPrimaryKey(m.AuthorId)!.Username, uow.Users.GetByPrimaryKey(m.AuthorId)!.Id)).ToList();
    }

    /// <inheritdoc />
    public MessageDTO CreateNiceMessage(string message, long userId)
    {
        using var uow = _dbService.UnitOfWork;
        return new MessageDTO(message, uow.Users.GetByPrimaryKey(userId)!.Username, uow.Users.GetByPrimaryKey(userId)!.Id);
    }
}