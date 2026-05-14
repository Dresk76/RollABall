namespace RollABall.Core.Events
{
    public readonly struct LevelCompletedEvent
    {
        public readonly int FinalScore;

        public LevelCompletedEvent(int finalScore)
        {
            FinalScore = finalScore;
        }
    }
}