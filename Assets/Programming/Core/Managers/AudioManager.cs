using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Domain.Enums;
using RollABall.Domain.Models;
using RollABall.Infrastructure.Save;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace RollABall.Core.Managers
{
    public class AudioManager : MonoBehaviour, IGlobalInitializable, IDisposable
    {
        // ─── Singleton ────────────────────────────────────────────────
        private static AudioManager _instance;
        public static AudioManager Instance { get; private set; }

        // ─── Campos ───────────────────────────────────────────────────
        [Header("Audio Sources")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;

        [Header("Audio Mixer")]
        [SerializeField] private AudioMixer _audioMixer;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip _backgroundMusic;

        private IEventBus _eventBus;
        private AudioModel _audioModel;
        private bool _initialized = false;

        private const string MusicParam = "music";
        private const string SfxParam   = "SFX";

        // ─── Ciclo de vida Unity ──────────────────────────────────────
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            Instance  = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            // AudioMixer necesita estar listo antes de recibir valores
            if (_initialized)
            {
                ApplyMusicVolume(_audioModel.MusicVolume);
                ApplySfxVolume(_audioModel.SfxVolume);
            }
        }

        // ─── Inicialización ───────────────────────────────────────────
        public void Initialize(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            var progress = SaveSystem.Load();
            _audioModel  = new AudioModel(progress.MusicVolume, progress.SfxVolume);

            _audioModel.OnMusicVolumeChanged += HandleMusicVolumeChanged;
            _audioModel.OnSfxVolumeChanged   += HandleSfxVolumeChanged;

            _eventBus.Subscribe<AudioVolumeChangedEvent>(HandleAudioVolumeChanged);
            _eventBus.Subscribe<LoadSceneEvent>(HandleLoadScene);

            _initialized = true;
        }

        // ─── Handlers ─────────────────────────────────────────────────
        private void HandleAudioVolumeChanged(AudioVolumeChangedEvent e)
        {
            if (e.IsMusic)
            {
                if (e.Level > _audioModel.MusicVolume)
                    _audioModel.IncreaseMusicVolume();
                else
                    _audioModel.DecreaseMusicVolume();
            }
            else
            {
                if (e.Level > _audioModel.SfxVolume)
                    _audioModel.IncreaseSfxVolume();
                else
                    _audioModel.DecreaseSfxVolume();
            }

            var progress         = SaveSystem.Load();
            progress.MusicVolume = _audioModel.MusicVolume;
            progress.SfxVolume   = _audioModel.SfxVolume;
            SaveSystem.Save(progress);
        }

        private void HandleLoadScene(LoadSceneEvent e)
        {
            if (e.SceneToLoad == SceneType.MainMenuScene)
            {
                _musicSource.clip = _backgroundMusic;
                _musicSource.Play();
            }
        }

        private void HandleMusicVolumeChanged(int level)
        {
            ApplyMusicVolume(level);
        }

        private void HandleSfxVolumeChanged(int level)
        {
            ApplySfxVolume(level);
        }

        // ─── API pública ──────────────────────────────────────────────
        public void PlaySfx(AudioClip clip)
        {
            _sfxSource.PlayOneShot(clip);
        }

        public AudioModel GetModel() => _audioModel;

        // ─── Helpers privados ─────────────────────────────────────────
        private void ApplyMusicVolume(int level)
        {
            _audioMixer.SetFloat(MusicParam, LevelToDecibels(level));
        }

        private void ApplySfxVolume(int level)
        {
            _audioMixer.SetFloat(SfxParam, LevelToDecibels(level));
        }

        private float LevelToDecibels(int level)
        {
            if (level == AudioModel.MinVolume) return -80f;
            return Mathf.Log10(level / (float)AudioModel.MaxVolume) * 20f;
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            _audioModel.OnMusicVolumeChanged -= HandleMusicVolumeChanged;
            _audioModel.OnSfxVolumeChanged   -= HandleSfxVolumeChanged;
            _eventBus?.Unsubscribe<AudioVolumeChangedEvent>(HandleAudioVolumeChanged);
            _eventBus?.Unsubscribe<LoadSceneEvent>(HandleLoadScene);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}