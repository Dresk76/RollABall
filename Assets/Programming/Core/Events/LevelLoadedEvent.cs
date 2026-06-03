namespace RollABall.Core.Events
{
    public readonly struct LevelLoadedEvent
    {
        public readonly int TotalKeys;
        public readonly int MaxScore;
        public readonly string LevelName;

        public LevelLoadedEvent(int totalKeys, int maxScore, string levelName)
        {
            TotalKeys  = totalKeys;
            MaxScore   = maxScore;
            LevelName  = levelName;
        }
    }
}