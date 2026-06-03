namespace RollABall.Core.Events
{
    public readonly struct LoadSpecificLevelRequestedEvent
    {
        public readonly int LevelIndex;

        public LoadSpecificLevelRequestedEvent(int levelIndex)
        {
            LevelIndex = levelIndex;
        }
    }
}