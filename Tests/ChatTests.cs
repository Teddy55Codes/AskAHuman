using AskAHuman.Services;
using AskAHuman.Services.Interfaces;
using DatabaseLayer;
using DatabaseLayer.Entities;
using DataBaseLayer.Repositories.Interfaces;
using DatabaseLayer.UnitOfWork;
using NSubstitute;

namespace Tests;

public class ChatTests
{
    private readonly IDbService _dbService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IChatRepository _chatRepository;
    private readonly IChatService _chatService;

    public ChatTests()
    {
        _dbService = Substitute.For<IDbService>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _chatRepository = Substitute.For<IChatRepository>();
        
        _dbService.UnitOfWork.Returns(_unitOfWork);
        _unitOfWork.Chats.Returns(_chatRepository);

        _chatService = new ChatService(_dbService);
    }

    [Fact]
    public void GetAllChatsAsCards_ShouldReturnAllChatCards()
    {
        // Arrange
        var chats = new List<Chat>
        {
            new() { Id = 1, Title = "Title1", Question = "Question1" },
            new() { Id = 2, Title = "Title2", Question = "Question2" }
        };
        
        _chatRepository.GetAll().Returns(chats);

        // Act
        var result = _chatService.GetAllChatsAsCards();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Title1", result[0].Title);
        Assert.Equal("Question1", result[0].Question);
        Assert.Equal("Title2", result[1].Title);
        Assert.Equal("Question2", result[1].Question);
    }

    [Fact]
    public void CreateNewChat_ShouldAddNewChat()
    {
        // Arrange
        var newChat = new Chat { Id = 1, UsersQuestioningId = 1, Title = "New Chat", Question = "New Question" };

        _chatRepository.Add(Arg.Any<Chat>()).Returns(newChat);

        // Act
        var result = _chatService.CreateNewChat(1, "New Chat", "New Question");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New Chat", result.Title);
        Assert.Equal("New Question", result.Question);
        Assert.Equal(1, result.UsersQuestioningId);
        _unitOfWork.Received(1).Commit();
    }

    [Fact]
    public void GetChatById_ShouldReturnNull_WhenChatDoesNotExist()
    {
        // Arrange
        _chatRepository.GetByPrimaryKey(Arg.Any<long>()).Returns((Chat)null);

        // Act
        var result = _chatService.GetChatById(1);

        // Assert
        Assert.Null(result);
    }
}