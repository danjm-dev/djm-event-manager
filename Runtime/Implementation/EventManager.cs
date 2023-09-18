namespace DJM.EventManager
{
    /// <summary>
    /// Event manager class responsible for managing generic struct events.
    /// Each event is identified by a struct type, which is also used as the event parameter for subscribed listeners.
    /// </summary>
    public sealed class EventManager : EventManagerBase, IEventManager
    {
    }
}