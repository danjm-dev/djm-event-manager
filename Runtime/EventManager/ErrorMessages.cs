using System;

namespace DJM.EventManager
{
    internal static class ErrorMessages
    {
        internal static string NullListenerSubscribed(Type eventId)
        {
            return $"Subscription attempt to {eventId} event had null listener.";
        }
        
        internal static string EventListenerTriggerException(Type eventId, Exception exception)
        {
            return $"Exception caught when triggering {eventId} event listener: {exception.Message}";
        }
    }
}