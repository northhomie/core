using System.Collections.Generic;

namespace Core.Events.EventBus
{
    public class SubscribersList<T> where T : class
    {
        public bool IsExecuting { get; set; }
        
        public readonly List<T> Subscribers = new List<T>();

        private bool _needsCleanUp = false;
        
        public void Add(T subscriber)
        {
            Subscribers.Add(subscriber);
        }

        public void Remove(T subscriber)
        {
            if (IsExecuting)
            {
                var i = Subscribers.IndexOf(subscriber);
                if (i >= 0)
                {
                    _needsCleanUp = true;
                    Subscribers[i] = null;
                }
            }
            else
            {
                Subscribers.Remove(subscriber);
            }
        }

        public void Cleanup()
        {
            if (!_needsCleanUp)
            {
                return;
            }

            Subscribers.RemoveAll(s => s == null);
            _needsCleanUp = false;
        }
    }
}