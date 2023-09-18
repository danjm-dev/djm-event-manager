namespace DJM.EventManager
{
    /// <summary>
    /// Event manager for centralized events.
    /// Each event is identified by a struct type, which is also used as the event parameter for subscribed listeners.
    /// </summary>
    public interface IEventManager : ISubscribableEventManager, ITriggerableEventManager, IClearableEventManager, IResettableEventManager
    {
    }
}