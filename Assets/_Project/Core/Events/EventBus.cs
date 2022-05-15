using System;
using System.Collections.Generic;

namespace Core.Events
{
    /// This class implements a variation of the Event Bus pattern
    /// Responsible for dispatch events between objects
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<object>> _events = new Dictionary<Type, List<object>>();
        
        public static void Subscribe<EventType>(Action<EventType> callback)
        {
            Type eventType = typeof(EventType);
            
            if (!_events.ContainsKey(eventType))
            {
                _events.Add(eventType, new List<object>());
            }

            // Associate event type with callbacks
            List<object> callbacks = _events[eventType];
            if (!callbacks.Contains(callback))
            {
                _events[eventType].Add(callback);
            }
        }
        
        public static void Raise<EventType>(EventType eventArgument)
        {
            Type eventType = typeof(EventType);
            
            // Check if we have any associated callbacks for this event
            if (!_events.ContainsKey(eventType))
            {
                return;
            }
            
            // Take callbacks associated with this event type and raise it
            List<object> callbacks = _events[eventType];
            foreach (Action<EventType> callback in callbacks)
            {
                callback.Invoke(eventArgument);
            }
        }

        public static void Unsubscribe<EventType>(Action<EventType> callback)
        {
            Type eventType = typeof(EventType);
            
            if (!_events.ContainsKey(eventType))
            {
                return;
            }
            
            List<object> callbacks = _events[eventType];
            callbacks.Remove(callback);

            // If event type does not have associated callbacks, remove it
            if (callbacks.Count == 0)
            {
                _events.Remove(eventType);
            }
        }
    }
}