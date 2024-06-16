using DataBaseLayer.DTOs;

namespace AskAHuman.Services.Interfaces;

public delegate void MessageSentEvent(string messageContent, long chatId, long authorId);
public delegate void MessageReceivedEvent(MessageDTO message);

public interface ILiveMessageService
{
    public long AssociatedChat { get; }
    public long AssociatedUser { get; }

    /// <summary>
    /// Set the <see cref="AssociatedChat"/> and <see cref="AssociatedUser"/> for the service.
    /// Needs to be set before messages can be sent.
    /// </summary>
    /// <param name="chatId">The id of the chat.</param>
    /// <param name="userId">The id of the user.</param>
    public void SetInformation(long chatId, long userId);

    /// <summary>
    /// Send a message in a chat.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="authorId">The id of the author of the message</param>
    public void SendMessageToChat(string message, long authorId);
    
    /// <summary>
    /// Used to receive messages from the <see cref="LiveMessageCoordinatorService"/>.
    /// </summary>
    /// <param name="msg">The received message.</param>
    public void ReceiveMessage(MessageDTO msg);
    
    
    public event MessageSentEvent OnMessageSent;
    public event MessageReceivedEvent OnMessageReceived;
}