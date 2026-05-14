using TMPro;
using UnityEngine;

namespace RollABall.Presentation.UI.Intro
{
    public class IntroView : MonoBehaviour
    {
        [SerializeField, Tooltip("Texto del tiempo")]
        private TextMeshProUGUI _countdownText;

        private void OnValidate()
        {
            Debug.Assert(_countdownText != null, nameof(_countdownText));
        }

        public void SetCountdownText(string value)
        {
            _countdownText.text = value;
        }
    }
}