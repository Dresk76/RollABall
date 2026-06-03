using UnityEngine;

namespace RollABall.Infrastructure.Configuration
{
    [CreateAssetMenu(fileName = "UI Level Entry Style", menuName = "Scriptable Objects/Config/UI Level Entry Style")]
    public class UILevelEntryStyle : ScriptableObject
    {
        [Header("COLORS")]
        [SerializeField] private Color _unlockedColor = Color.white;
        [SerializeField] private Color _lockedColor   = new(0.4f, 0.4f, 0.4f, 1f);

        public Color UnlockedColor => _unlockedColor;
        public Color LockedColor   => _lockedColor;
    }
}