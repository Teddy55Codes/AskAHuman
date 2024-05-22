using System.Collections.Concurrent;

namespace AskAHuman.Services.Interfaces;

public interface ILiveMessageCoordinatorService
{
    public void AddInstance(ILiveMessageService liveMessageService);
    public void RemoveInstance(ILiveMessageService liveMessageService);
}