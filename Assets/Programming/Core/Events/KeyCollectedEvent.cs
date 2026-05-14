namespace RollABall.Core.Events
{
    public readonly struct KeyCollectedEvent
    {
        public readonly int KeyValue;

        public KeyCollectedEvent(int keyValue)
        {
            KeyValue = keyValue;
        }
    }
}