namespace RollABall.Core.Events
{
    public readonly struct KeysCountChangedEvent
    {
        public readonly int TotalKeys;

        public KeysCountChangedEvent(int totalKeys)
        {
            TotalKeys = totalKeys;
        }
    }
}