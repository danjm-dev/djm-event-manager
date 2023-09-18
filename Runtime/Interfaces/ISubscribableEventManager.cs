using System;

namespace DJM.EventManager
{
    public interface ISubscribableEventManager
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
    }
}