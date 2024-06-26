﻿using DataBaseLayer.DTOs;
using DatabaseLayer.Entities;
using FluentResults;

namespace AskAHuman.Services.Interfaces;

public interface IChatService
{
    /// <summary>
    /// Gets all chats formatted for chat cards.
    /// </summary>
    /// <returns>All chats as <see cref="ChatCardDTO"/>.</returns>
    public List<ChatCardDTO> GetAllChatsAsCards();
    
    /// <summary>
    /// Gets all unanswered chats formatted for chat cards.
    /// </summary>
    /// <returns>Unanswered chats as <see cref="ChatCardDTO"/>.</returns>
    public List<ChatCardDTO> GetUnansweredChatsAsCards();
    
    /// <summary>
    /// Gets all active chats from a user formatted for chat cards.
    /// </summary>
    /// <param name="userId">The user to get the chats for.</param>
    /// <returns>chats as <see cref="ChatCardDTO"/>.</returns>
    public List<ChatCardDTO> GetUsersActiveChatsAsCards(long userId);
    
    /// <summary>
    /// Gets all completed chats from a user formatted for chat cards.
    /// </summary>
    /// <param name="userId">The user to get the chats for.</param>
    /// <returns>chats as <see cref="ChatCardDTO"/>.</returns>
    public List<ChatCardDTO> GetUsersCompletedChatsAsCards(long userId);
    
    /// <summary>
    /// Creates a new chat.
    /// </summary>
    /// <param name="userId">The user who creates the chat.</param>
    /// <param name="title">The title of the chat.</param>
    /// <param name="question">The question being asked in the chat.</param>
    /// <returns>The result for the new chat.</returns>
    public  Result<Chat> CreateNewChat(long userId, string title, string question);

    /// <summary>
    /// Complete a question
    /// </summary>
    /// <param name="userId">The user requesting to close a question.</param>
    /// <param name="chatId">The chat to complete.</param>
    /// <param name="wasQuestionSolved">If the question was solved or the question is just closed.</param>
    /// <returns>The result for the closing of the question.</returns> 
    public Result CompleteChat(long userId, long chatId, bool wasQuestionSolved);
    
    /// <summary>
    /// User sets themselves as the answerer of a chat. 
    /// </summary>
    /// <param name="userId">The user to set as <see cref="Chat.UsersAnswerer"/>.</param>
    /// <param name="chatId">The chat to set the <see cref="Chat.UsersAnswerer"/> on.</param>
    /// <returns>The result for the claimed chat.</returns>
    public Result<Chat> ClaimChat(long userId, long chatId);
    
    /// <summary>
    /// User removes themselves as the answerer of a chat. 
    /// </summary>
    /// <param name="chatId">The chat to remove the <see cref="Chat.UsersAnswerer"/> from.</param>
    /// <returns>The result for the claimed chat.</returns>
    public Result<Chat> RemoveAnswererFromChat(long chatId);

    /// <summary>
    /// Gets a chat by id.
    /// </summary>
    /// <param name="chatId">The chat id.</param>
    /// <returns>The chat if a chat with this id exists.</returns>
    public Chat? GetChatById(long chatId);
}