using System;

namespace DJM.EventManager
{
    /// <summary>
    /// Event service interface for centralized events.
    /// Each event is identified by a struct type, which is also used as the event parameter for subscribed listeners.
    /// </summary>
    public interface IEventManager
    {
        /// <summary>
        /// Subscribes a listener to an event, identified and parameterized by the struct type T.
        /// </summary>
        /// <param name="listener">The action to be invoked when the event is triggered.</param>
        /// <typeparam name="T">The struct type that both identifies the event and serves as the parameter for the listener.</typeparam>
        public void Subscribe<T>(Action<T> listener) where T : struct;
        
        /// <summary>
        /// Unsubscribes a listener from an event, identified and parameterized by the struct type T.
        /// </summary>
        /// <param name="listener">The action to be removed from the event's invocation list.</param>
        /// <typeparam name="T">The struct type that both identifies the event and serves as the parameter for the listener.</typeparam>
        public void Unsubscribe<T>(Action<T> listener) where T : struct;
        
        /// <summary>
        /// Triggers an event, identified and parameterized by the struct type T.
        /// </summary>
        /// <param name="eventInstance">An instance of type T that will be passed as the parameter to all subscribed listeners.</param>
        /// <typeparam name="T">The struct type that both identifies the event and serves as the parameter for the listener.</typeparam>
        public void TriggerEvent<T>(T eventInstance) where T : struct;
        
        /// <summary>
        /// Removes all listeners from all events.
        /// </summary>
        public void ClearAllEvents();

        /// <summary>
        /// Removes all listeners from specified event.
        /// </summary>
        /// <typeparam name="T">The type of event to remove listeners from. Must be a struct.</typeparam>
        public void ClearEvent<T>() where T : struct;
    }
}