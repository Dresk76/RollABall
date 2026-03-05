using RollABall.Programming.Core.Events;

namespace RollABall.Programming.Core.Interfaces
{
    public interface IGlobalInitializable
    {
        void Initialize(IEventBus eventBus);
    }
}
