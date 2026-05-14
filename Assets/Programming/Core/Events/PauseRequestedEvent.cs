namespace RollABall.Core.Events
{
    public readonly struct PauseRequestedEvent
    {
        public readonly bool IsPausing;

        public PauseRequestedEvent(bool isPausing)
        {
            IsPausing = isPausing;
        }
    }
}