using DataBaseLayer.DTOs;
using DatabaseLayer.Entities;

namespace AskAHuman.Services.Interfaces;

public interface IChatService
{
    /// <summary>
    /// Gets all chats formatted for chat cards.
    /// </summary>
    /// <returns>All chats as <see cref="ChatCardDTO"/>.</returns>
    public List<ChatCardDTO> GetAllChatsAsCards();
    
    /// <summary>
    /// Creates a new chat.
    /// </summary>
    /// <param name="userId">The user who creates the chat.</param>
    /// <param name="title">The title of the chat.</param>
    /// <param name="question">The question being asked in the chat.</param>
    /// <returns>The new chat.</returns>
    public Chat CreateNewChat(long userId, string title, string question);

    /// <summary>
    /// Gets a chat by id.
    /// </summary>
    /// <param name="chatId">The chat id.</param>
    /// <returns>The chat if a chat with this id exists.</returns>
    public Chat? GetChatById(long chatId);
}