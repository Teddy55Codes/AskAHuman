namespace AskAHuman.Services.Interfaces;

public interface ILiveMessageCoordinatorService
{
    /// <summary>
    /// Register a new <see cref="ILiveMessageService"/>.
    /// </summary>
    /// <param name="liveMessageService">The <see cref="ILiveMessageService"/> to register.</param>
    public void AddInstance(ILiveMessageService liveMessageService);
    
    /// <summary>
    /// Un-register a <see cref="ILiveMessageService"/>.
    /// </summary>
    /// <param name="liveMessageService">The <see cref="ILiveMessageService"/> to un-register.</param>
    public void RemoveInstance(ILiveMessageService liveMessageService);
}