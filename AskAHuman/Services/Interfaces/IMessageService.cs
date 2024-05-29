using DataBaseLayer.DTOs;

namespace AskAHuman.Services.Interfaces;

public interface IMessageService
{
    /// <summary>
    /// Get all current messages for a chat.
    /// </summary>
    /// <param name="chatId">The id of the chat to get the messages from.</param>
    /// <returns>All messages for the chat.</returns>
    public List<MessageDTO> GetMessagesForChatById(long chatId);
    
    /// <summary>
    /// Formats a message with aditional data.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="userId"></param>
    /// <returns>The formatted message.</returns>
    public MessageDTO CreateNiceMessage(string message, long userId);
}