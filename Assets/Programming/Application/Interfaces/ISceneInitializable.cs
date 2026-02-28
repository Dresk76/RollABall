using RollABall.Programming.Core.Events;

namespace RollABall.Programming.Core.Interfaces
{
    public interface ISceneInitializable
    {
        void Initialize(IEventBus eventBus);
    }
}