using UnityEngine;

namespace RollABall.Infrastructure.Configuration
{
    [CreateAssetMenu(fileName = "UI Button Style", menuName = "Scriptable Objects/Config/UI Button Style")]
    public class UIButtonStyle : ScriptableObject
    {
        public Color NormalColor;
        public Color HoverColor;
        public Color PressedColor;
    }
}