using RollABall.Infrastructure.Configuration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RollABall.Presentation.UI.Confirmation
{
    public class ConfirmationPanelView : MonoBehaviour
    {
        [Header("TEXTS")]
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private TextMeshProUGUI _confirmButtonText;
        [SerializeField] private TextMeshProUGUI _cancelButtonText;

        [Header("BUTTONS")]
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;

        [Header("CONFIGURATION")]
        [SerializeField] private UIMainMenuTexts _texts;

        public Button ConfirmButton => _confirmButton;
        public Button CancelButton  => _cancelButton;

        private void OnValidate()
        {
            Debug.Assert(_titleText != null, nameof(_titleText));
            Debug.Assert(_messageText != null, nameof(_messageText));
            Debug.Assert(_confirmButtonText != null, nameof(_confirmButtonText));
            Debug.Assert(_cancelButtonText != null, nameof(_cancelButtonText));
            Debug.Assert(_confirmButton != null, nameof(_confirmButton));
            Debug.Assert(_cancelButton != null, nameof(_cancelButton));
            Debug.Assert(_texts != null, nameof(_texts));
        }

        private void Awake()
        {
            _titleText.text         = _texts.ConfirmationTitleText;
            _messageText.text       = _texts.ConfirmationMessageText;
            _confirmButtonText.text = _texts.ConfirmText;
            _cancelButtonText.text  = _texts.CancelText;
        }
    }
}