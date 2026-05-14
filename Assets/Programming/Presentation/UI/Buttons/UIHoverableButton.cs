using RollABall.Infrastructure.Configuration;
using TMPro;
using UnityEngine;

namespace RollABall.Presentation.UI.Buttons
{
    public class UIHoverableButton : MonoBehaviour
    {
        [SerializeField] private UIHoverable _hoverable;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private UIButtonStyle _style;

        public UIHoverable Hoverable => _hoverable;
        public TextMeshProUGUI Text => _text;
        public UIButtonStyle Style => _style;

        private void OnValidate()
        {
            Debug.Assert(_hoverable != null, nameof(_hoverable));
            Debug.Assert(_text != null, nameof(_text));
            Debug.Assert(_style != null, nameof(_style));
        }
    }
}