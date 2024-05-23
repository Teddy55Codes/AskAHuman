using AskAHuman.Services;
using AskAHuman.Services.Interfaces;
using DatabaseLayer;
using DataBaseLayer.Repositories.Interfaces;
using DatabaseLayer.UnitOfWork;
using NSubstitute;

namespace Tests;

public class MessageTests
{
    private readonly IDbService _dbService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;
    private readonly LiveMessageService _liveMessageService;
    private readonly ILiveMessageCoordinatorService _liveMessageCoordinatorService;

    public MessageTests()
    {
        _dbService = Substitute.For<IDbService>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _messageRepository = Substitute.For<IMessageRepository>();
        _chatRepository = Substitute.For<IChatRepository>();
        _liveMessageCoordinatorService = Substitute.For<ILiveMessageCoordinatorService>();
        
        _liveMessageService = new LiveMessageService(_dbService, _liveMessageCoordinatorService);
    }

    [Fact]
    public void SetInformation_ShouldRegisterToCoordinator()
    {
        // Act
        _liveMessageService.SetInformation(1, 1);

        // Assert
        _liveMessageCoordinatorService.Received(1).AddInstance(_liveMessageService);
    }

    [Fact]
    public void Dispose_ShouldUnregisterFromCoordinator()
    {
        // Arrange
        _liveMessageService.SetInformation(1, 1);

        // Act
        _liveMessageService.Dispose();

        // Assert
        _liveMessageCoordinatorService.Received(1).RemoveInstance(_liveMessageService);
    }
}