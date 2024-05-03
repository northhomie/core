using System;
using System.Collections.Generic;
using System.Linq;
using Core.Events.Handlers;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Events.EventBus
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, SubscribersList<IEventHandler>> Subscribers = new Dictionary<Type, SubscribersList<IEventHandler>>();

        public static void Subscribe(IEventHandler subscriber)
        {
            var handlers = GetAllEventHandlers(subscriber);

            foreach (var handler in handlers)
            {
                if (!Subscribers.ContainsKey(handler))
                {
                    Subscribers[handler] = new SubscribersList<IEventHandler>();
                }
                
                Subscribers[handler].Add(subscriber);
            }
        }

        public static void Unsubscribe(IEventHandler subscriber)
        {
            var handlers = GetAllEventHandlers(subscriber);
            
            foreach (var handler in handlers)
            {
                if (Subscribers.ContainsKey(handler))
                {
                    Subscribers[handler].Remove(subscriber);
                }
            }
        }

        public static void Fire<T>(UnityAction<T> action) where T : IEventHandler
        {
            var subscribersList = Subscribers[typeof(T)];
            subscribersList.IsExecuting = true;

            foreach (var subscriber in subscribersList.Subscribers)
            {
                try
                {
                    action.Invoke(subscriber is T handler ? handler : default);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            
            subscribersList.IsExecuting = false;
            subscribersList.Cleanup();
        }

        private static List<Type> GetAllEventHandlers(IEventHandler subscriber)
        {
            var handlers = subscriber
                .GetType()
                .GetInterfaces()
                .Where(it => typeof(IEventHandler).IsAssignableFrom(it) && it != typeof(IEventHandler))
                .ToList();

            return handlers;
        }
    }
}