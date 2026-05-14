namespace RollABall.Core.Events
{
    public readonly struct TimerUpdatedEvent
    {
        public readonly float ElapsedSeconds;

        public TimerUpdatedEvent(float elapsedSeconds)
        {
            ElapsedSeconds = elapsedSeconds;
        }
    }
}