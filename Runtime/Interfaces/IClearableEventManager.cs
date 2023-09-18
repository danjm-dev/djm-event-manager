namespace DJM.EventManager
{
    public interface IClearableEventManager
    {
        /// <summary>
        /// Clears listeners from specified event.
        /// </summary>
        /// <typeparam name="T">The type of event to remove listeners from. Must be a struct.</typeparam>
        public void ClearEvent<T>() where T : struct;
    }
}