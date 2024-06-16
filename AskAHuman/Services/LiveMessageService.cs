using AskAHuman.Services.Interfaces;
using DatabaseLayer;
using DataBaseLayer.DTOs;

namespace AskAHuman.Services;

public class LiveMessageService : ILiveMessageService, IDisposable
{
    private readonly IDbService _dbService;
    private readonly ILiveMessageCoordinatorService _messageCoordinatorService;
    
    public event MessageSentEvent OnMessageSent;
    public event MessageReceivedEvent OnMessageReceived;
    
    public long AssociatedChat { get; private set;  }
    
    public long AssociatedUser { get; private set; }
    
    public LiveMessageService(IDbService dbService, ILiveMessageCoordinatorService messageCoordinatorService)
    {
        _dbService = dbService;
        _messageCoordinatorService = messageCoordinatorService;
    }
    
    /// <inheritdoc />
    public void SetInformation(long chatId, long userId)
    {
        AssociatedChat = chatId;
        AssociatedUser = userId;
        _messageCoordinatorService.AddInstance(this);
    }

    /// <inheritdoc />
    public void SendMessageToChat(string message, long authorId)
    {
        OnMessageSent?.Invoke(message, AssociatedChat, authorId);
    }

    /// <inheritdoc />
    public void ReceiveMessage(MessageDTO message)
    {
        OnMessageReceived?.Invoke(message);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (AssociatedChat != 0) _messageCoordinatorService.RemoveInstance(this);
    }
}