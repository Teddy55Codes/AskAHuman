using DataBaseLayer.DTOs;
using DatabaseLayer.Entities;

namespace AskAHuman.Services.Interfaces;

public interface IChatService
{
    public List<ChatCardDTO> GetAllChatsAsCards();
    public Chat CreateNewChat(long userId, string title, string question);

    public Chat? GetChatById(long chatId);
}