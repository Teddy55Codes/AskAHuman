using DataBaseLayer.DTOs;

namespace AskAHuman.Services.Interfaces;

public interface IMessageService
{
    public List<MessageDTO> GetMessagesForChatById(long chatId);
    public MessageDTO CreateNiceMessage(string message, long userId);
}