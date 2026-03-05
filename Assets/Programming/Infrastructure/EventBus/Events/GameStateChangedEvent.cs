using RollABall.Programming.Data.Enums;

namespace RollABall.Programming.Core.Events
{
    public struct GameStateChangedEvent
    {
        public readonly GameState NewState;

        public GameStateChangedEvent(GameState newState)
        {
            NewState = newState;
        }
    }
}
