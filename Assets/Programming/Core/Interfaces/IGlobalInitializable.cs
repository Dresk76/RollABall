using RollABall.Core.Events;

namespace RollABall.Core.Interfaces
{
    public interface IGlobalInitializable
    {
        void Initialize(IEventBus eventBus);
    }
}