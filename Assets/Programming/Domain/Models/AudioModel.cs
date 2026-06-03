using System;

namespace RollABall.Domain.Models
{
    public class AudioModel
    {
        // ─── Constantes ───────────────────────────────────────────────
        public const int MinVolume     = 0;
        public const int MaxVolume     = 5;
        public const int DefaultVolume = 3;

        // ─── Campos ───────────────────────────────────────────────────
        private int _musicVolume;
        private int _sfxVolume;

        public int MusicVolume => _musicVolume;
        public int SfxVolume   => _sfxVolume;

        public event Action<int> OnMusicVolumeChanged;
        public event Action<int> OnSfxVolumeChanged;

        public AudioModel(int musicVolume, int sfxVolume)
        {
            _musicVolume = Clamp(musicVolume);
            _sfxVolume   = Clamp(sfxVolume);
        }

        public void IncreaseMusicVolume()
        {
            if (_musicVolume >= MaxVolume) return;
            _musicVolume++;
            OnMusicVolumeChanged?.Invoke(_musicVolume);
        }

        public void DecreaseMusicVolume()
        {
            if (_musicVolume <= MinVolume) return;
            _musicVolume--;
            OnMusicVolumeChanged?.Invoke(_musicVolume);
        }

        public void IncreaseSfxVolume()
        {
            if (_sfxVolume >= MaxVolume) return;
            _sfxVolume++;
            OnSfxVolumeChanged?.Invoke(_sfxVolume);
        }

        public void DecreaseSfxVolume()
        {
            if (_sfxVolume <= MinVolume) return;
            _sfxVolume--;
            OnSfxVolumeChanged?.Invoke(_sfxVolume);
        }

        private int Clamp(int value)
        {
            return Math.Max(MinVolume, Math.Min(MaxVolume, value));
        }
    }
}