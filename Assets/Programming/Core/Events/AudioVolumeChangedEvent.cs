namespace RollABall.Core.Events
{
    public readonly struct AudioVolumeChangedEvent
    {
        public readonly bool IsMusic;
        public readonly int Level;

        public AudioVolumeChangedEvent(bool isMusic, int level)
        {
            IsMusic = isMusic;
            Level   = level;
        }
    }
}