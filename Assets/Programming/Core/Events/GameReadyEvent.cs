namespace RollABall.Core.Events
{
    public readonly struct GameReadyEvent
    {
        public readonly bool HasActiveGame;

        public GameReadyEvent(bool hasActiveGame)
        {
            HasActiveGame = hasActiveGame;
        }
    }
}