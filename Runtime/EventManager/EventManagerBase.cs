using System;
using System.Collections.Generic;
using UnityEngine;

namespace DJM.EventManager
{
    /// <summary>
    /// Base abstract event manager implementation for classes implementing <see cref="IEventManager"/>.
    /// Each event is identified by a struct type, which is also used as the event parameter for subscribed listeners.
    /// </summary>
    public abstract class EventManagerBase
    {
        private readonly Dictionary<Type, Delegate> _eventDictionary = new();

        public virtual void Subscribe<T>(Action<T> listener) where T : struct
        {
            var eventId = typeof(T);
            
            if (listener is null)
            {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                Debug.LogError(ErrorMessages.NullListenerSubscribed(eventId));
                return;
#endif
            }
            
            if (_eventDictionary.ContainsKey(eventId))
            {
                _eventDictionary[eventId] = Delegate.Combine(_eventDictionary[eventId], listener);
                return;
            }

            _eventDictionary[eventId] = listener;
        }
        
        public virtual void Unsubscribe<T>(Action<T> listener) where T : struct
        {
            var eventId = typeof(T);
            if (!_eventDictionary.ContainsKey(eventId)) return;
            _eventDictionary[eventId] = Delegate.Remove(_eventDictionary[eventId], listener);
            
            if (_eventDictionary[eventId] is null) _eventDictionary.Remove(eventId);
        }
        
        public virtual void TriggerEvent<T>(T eventInstance) where T : struct
        {
            var eventId = typeof(T);
            if (!_eventDictionary.ContainsKey(eventId)) return;
            
            foreach (var listener in _eventDictionary[eventId].GetInvocationList())
            {
                try
                {
                    ((Action<T>)listener)?.Invoke(eventInstance);
                }
                catch (Exception exception)
                {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                    Debug.LogError(ErrorMessages.EventListenerTriggerException(eventId, exception));
#endif
                }
            }
        }
        
        public void ClearAllEvents() => _eventDictionary.Clear();
        
        public void ClearEvent<T>() where T : struct
        {
            var eventId = typeof(T);
            _eventDictionary.Remove(eventId);
        }
    }
}