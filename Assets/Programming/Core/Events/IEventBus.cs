using System;

namespace RollABall.Core.Events
{
    public interface IEventBus
    {
        void Subscribe<T>(Action<T> callback) where T : struct;
        void Unsubscribe<T>(Action<T> callback) where T : struct;
        void Publish<T>(T eventData) where T : struct;
        void Clear();
        bool HasSubscribers<T>() where T : struct;
    }
}