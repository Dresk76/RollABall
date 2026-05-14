using System;
using System.Collections.Generic;

namespace RollABall.Core.Events
{
    public sealed class EventBus : IEventBus
    {
        private readonly Dictionary<Type, Delegate> _events = new();

        public void Subscribe<T>(Action<T> callback) where T : struct
        {
            var type = typeof(T);

            if (_events.TryGetValue(type, out var existingDelegate))
                _events[type] = Delegate.Combine(existingDelegate, callback);
            else
                _events[type] = callback;
        }

        public void Unsubscribe<T>(Action<T> callback) where T : struct
        {
            var type = typeof(T);

            if (!_events.TryGetValue(type, out var existingDelegate))
                return;

            var newDelegate = Delegate.Remove(existingDelegate, callback);

            if (newDelegate == null)
                _events.Remove(type);
            else
                _events[type] = newDelegate;
        }

        public void Publish<T>(T eventData) where T : struct
        {
            var type = typeof(T);

            if (_events.TryGetValue(type, out var existingDelegate))
            {
                var callback = existingDelegate as Action<T>;
                callback?.Invoke(eventData);
            }
        }

        public void Clear()
        {
            _events.Clear();
        }

        public bool HasSubscribers<T>() where T : struct
        {
            return _events.ContainsKey(typeof(T));
        }
    }
}