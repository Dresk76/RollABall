namespace RollABall.Core.Events
{
    public readonly struct LevelLoadedEvent
    {
        public readonly int TotalKeys;
        public readonly int MaxScore;

        public LevelLoadedEvent(int totalKeys, int maxScore)
        {
            TotalKeys = totalKeys;
            MaxScore = maxScore;
        }
    }
}