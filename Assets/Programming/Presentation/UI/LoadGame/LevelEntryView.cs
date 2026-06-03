using RollABall.Infrastructure.Configuration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RollABall.Presentation.UI.LoadGame
{
    public class LevelEntryView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _levelImage;
        [SerializeField] private TextMeshProUGUI _levelNameText;
        [SerializeField] private GameObject _lockIcon;

        [Header("CONFIGURATION")]
        [SerializeField] private UILevelEntryStyle _style;

        public Button Button => _button;

        private void OnValidate()
        {
            Debug.Assert(_button != null, nameof(_button));
            Debug.Assert(_levelImage != null, nameof(_levelImage));
            Debug.Assert(_levelNameText != null, nameof(_levelNameText));
            Debug.Assert(_lockIcon != null, nameof(_lockIcon));
            Debug.Assert(_style != null, nameof(_style));
        }

        public void Setup(string levelName, Sprite levelSprite, bool isUnlocked)
        {
            _levelNameText.text  = levelName;
            _levelImage.sprite   = levelSprite;
            _button.interactable = isUnlocked;
            _lockIcon.SetActive(!isUnlocked);
            _levelImage.color    = isUnlocked ? _style.UnlockedColor : _style.LockedColor;
        }
    }
}