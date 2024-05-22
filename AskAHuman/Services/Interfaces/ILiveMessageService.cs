using DataBaseLayer.DTOs;

namespace AskAHuman.Services.Interfaces;

public delegate void MessageSentEvent(string messageContent, long chatId, long authorId);
public delegate void MessageReceivedEvent(MessageDTO message);

public interface ILiveMessageService
{
    public long AssociatedChat { get; }
    public long AssociatedUser { get; }

    public void SetInformation(long chatId, long userId);

    public void SendMessageToChat(string message, long authorId);

    public void ReceiveMessage(MessageDTO msg);
    
    
    public event MessageSentEvent OnMessageSent;
    public event MessageReceivedEvent OnMessageReceived;
}