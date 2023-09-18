namespace DJM.EventManager
{
    public interface ITriggerableEventManager
    {
        /// <summary>
        /// Triggers an event, identified and parameterized by the struct type T.
        /// </summary>
        /// <param name="eventInstance">An instance of type T that will be passed as the parameter to all subscribed listeners.</param>
        /// <typeparam name="T">The struct type that both identifies the event and serves as the parameter for the listener.</typeparam>
        public void TriggerEvent<T>(T eventInstance) where T : struct;
    }
}