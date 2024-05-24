using System.Collections.Concurrent;
using AskAHuman.Services.Interfaces;
using DatabaseLayer;
using DatabaseLayer.Entities;

namespace AskAHuman.Services;

public class LiveMessageCoordinatorService : ILiveMessageCoordinatorService
{
    private readonly IDbService _dbService;
    private readonly IMessageService _messageService;
    private ConcurrentDictionary<long, List<ILiveMessageService>> _messageServicesInstances = new();

    public LiveMessageCoordinatorService(IDbService dbService, IMessageService messageService)
    {
        _dbService = dbService;
        _messageService = messageService;
    }
    
    public void AddInstance(ILiveMessageService liveMessageService)
    {
        _messageServicesInstances.AddOrUpdate(
            liveMessageService.AssociatedChat,
            _ => (List<ILiveMessageService>) [liveMessageService],
            (_, list) =>
            {
                list.Add(liveMessageService);
                return list;
            });
        
        liveMessageService.OnMessageSent += PrccessMessage;
    }

    public void RemoveInstance(ILiveMessageService liveMessageService)
    {
        _messageServicesInstances.AddOrUpdate(
            liveMessageService.AssociatedChat,
            _ => new List<ILiveMessageService>(),
            (_, list) =>
            {
                list.Remove(liveMessageService);
                return list;
            });

        liveMessageService.OnMessageSent -= PrccessMessage;
    }

    private void PrccessMessage(string message, long chatId, long authorId)
    {
        var msg = _messageService.CreateNiceMessage(message, authorId);
        foreach (var instance in _messageServicesInstances[chatId])
        {
            instance.ReceiveMessage(msg);
        }

        using var uow = _dbService.UnitOfWork;
        uow.Messages.Add(new Message
        {
            Content = message,
            AuthorId = authorId,
            ChatId = chatId
        });
        uow.Commit();
    }
}