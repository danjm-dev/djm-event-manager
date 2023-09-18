namespace DJM.EventManager
{
    public interface IResettableEventManager
    {
        /// <summary>
        /// Removes all listeners from all events.
        /// </summary>
        public void ClearAllEvents();
    }
}