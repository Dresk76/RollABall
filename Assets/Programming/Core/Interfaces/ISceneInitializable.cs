using RollABall.Core.Events;

namespace RollABall.Core.Interfaces
{
    public interface ISceneInitializable
    {
        void Initialize(IEventBus eventBus);
    }
}