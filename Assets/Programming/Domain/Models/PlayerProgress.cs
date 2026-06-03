using System;
using System.Collections.Generic;

namespace RollABall.Domain.Models
{
    [Serializable]
    public class PlayerProgress
    {
        public bool HasPlayedBefore;
        public bool IntroductionCompleted;
        public int LastCompletedLevel;
        public int NextLevelToPlay;
        public int TotalScore;
        public int MusicVolume = AudioModel.DefaultVolume;
        public int SfxVolume = AudioModel.DefaultVolume;
        public List<int> UnlockedLevels = new();
    }
}