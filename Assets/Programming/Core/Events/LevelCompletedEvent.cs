namespace RollABall.Core.Events
{
    public readonly struct LevelCompletedEvent
    {
        public readonly int FinalScore;
        public readonly int Keys;
        public readonly float ElapsedSeconds;

        public LevelCompletedEvent(int finalScore, int keys, float elapsedSeconds)
        {
            FinalScore     = finalScore;
            Keys           = keys;
            ElapsedSeconds = elapsedSeconds;
        }
    }
}