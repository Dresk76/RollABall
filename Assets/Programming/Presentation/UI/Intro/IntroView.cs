using UnityEngine;
using TMPro;

namespace RollABall.Presentation.UI.Intro
{
    public class IntroView : MonoBehaviour
    {
        [SerializeField, Tooltip("Texto del tiempo.")] private TextMeshProUGUI _countdownText;


        public void SetCountdownText(string value)
        {
            _countdownText.text = value;
        }
    }
}
