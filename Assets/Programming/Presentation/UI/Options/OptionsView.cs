using RollABall.Domain.Models;
using RollABall.Infrastructure.Configuration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RollABall.Presentation.UI.Options
{
    public class OptionsView : MonoBehaviour
    {
        [Header("TEXTS")]
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _musicLabelText;
        [SerializeField] private TextMeshProUGUI _sfxLabelText;

        [Header("MUSIC")]
        [SerializeField] private Button _musicVolumeDownButton;
        [SerializeField] private Button _musicVolumeUpButton;
        [SerializeField] private Image[] _musicVolume;

        [Header("SFX")]
        [SerializeField] private Button _sfxVolumeDownButton;
        [SerializeField] private Button _sfxVolumeUpButton;
        [SerializeField] private Image[] _sfxVolume;

        [Header("NAVIGATION")]
        [SerializeField] private Button _backButton;

        [Header("SPRITES")]
        [SerializeField] private Sprite _sphereFull;
        [SerializeField] private Sprite _sphereEmpty;

        [Header("CONFIGURATION")]
        [SerializeField] private UIMainMenuTexts _texts;

        public Button MusicVolumeDownButton => _musicVolumeDownButton;
        public Button MusicVolumeUpButton   => _musicVolumeUpButton;
        public Button SfxVolumeDownButton   => _sfxVolumeDownButton;
        public Button SfxVolumeUpButton     => _sfxVolumeUpButton;
        public Button BackButton            => _backButton;

        private void OnValidate()
        {
            Debug.Assert(_titleText != null, nameof(_titleText));
            Debug.Assert(_musicLabelText != null, nameof(_musicLabelText));
            Debug.Assert(_sfxLabelText != null, nameof(_sfxLabelText));
            Debug.Assert(_musicVolumeDownButton != null, nameof(_musicVolumeDownButton));
            Debug.Assert(_musicVolumeUpButton != null, nameof(_musicVolumeUpButton));
            Debug.Assert(_sfxVolumeDownButton != null, nameof(_sfxVolumeDownButton));
            Debug.Assert(_sfxVolumeUpButton != null, nameof(_sfxVolumeUpButton));
            Debug.Assert(_backButton != null, nameof(_backButton));
            Debug.Assert(_sphereFull != null, nameof(_sphereFull));
            Debug.Assert(_sphereEmpty != null, nameof(_sphereEmpty));
            Debug.Assert(_texts != null, nameof(_texts));
        }

        private void Awake()
        {
            _titleText.text      = _texts.OptionsTitleText;
            _musicLabelText.text = _texts.MusicLabelText;
            _sfxLabelText.text   = _texts.SfxLabelText;
        }

        public void UpdateMusicVolume(int level)
        {
            UpdateVolume(_musicVolume, level);
            _musicVolumeDownButton.interactable = level > AudioModel.MinVolume;
            _musicVolumeUpButton.interactable   = level < AudioModel.MaxVolume;
        }

        public void UpdateSfxVolume(int level)
        {
            UpdateVolume(_sfxVolume, level);
            _sfxVolumeDownButton.interactable = level > AudioModel.MinVolume;
            _sfxVolumeUpButton.interactable   = level < AudioModel.MaxVolume;
        }

        private void UpdateVolume(Image[] spheres, int level)
        {
            for (int i = 0; i < spheres.Length; i++)
            {
                if (spheres[i] == null) return; // ← verifica que la imagen existe
                spheres[i].sprite = i < level ? _sphereFull : _sphereEmpty;
            }
        }
    }
}