using RollABall.Domain.Enums;
using System;
using UnityEngine;

namespace RollABall.Infrastructure.Configuration
{
    [CreateAssetMenu(fileName = "Level Progression Config", menuName = "Scriptable Objects/Config/Level Progression Config")]
    public class LevelProgressionConfig : ScriptableObject
    {
        [Serializable]
        public struct LevelEntry
        {
            public int LevelIndex;
            public string LevelName;
            public SceneType SceneType;
            public Sprite LevelSprite; // ← imagen del nivel para el carrusel
        }

        [SerializeField] private LevelEntry[] _levels;

        public bool TryGetNextLevel(int currentLevelIndex, out SceneType nextScene)
        {
            for (int i = 0; i < _levels.Length; i++)
            {
                if (_levels[i].LevelIndex == currentLevelIndex)
                {
                    if (i + 1 < _levels.Length)
                    {
                        nextScene = _levels[i + 1].SceneType;
                        return true;
                    }
                }
            }

            nextScene = SceneType.MainMenuScene;
            return false;
        }

        public bool TryGetLevelByIndex(int levelIndex, out SceneType scene)
        {
            foreach (var entry in _levels)
            {
                if (entry.LevelIndex == levelIndex)
                {
                    scene = entry.SceneType;
                    return true;
                }
            }

            scene = SceneType.MainMenuScene;
            return false;
        }

        public string GetLevelName(int levelIndex)
        {
            foreach (var entry in _levels)
            {
                if (entry.LevelIndex == levelIndex)
                    return entry.LevelName;
            }
            return $"Level {levelIndex + 1}";
        }

        public Sprite GetLevelSprite(int levelIndex)
        {
            foreach (var entry in _levels)
            {
                if (entry.LevelIndex == levelIndex)
                    return entry.LevelSprite;
            }
            return null;
        }

        public SceneType GetFirstLevel()
        {
            return _levels[0].SceneType;
        }

        public int TotalLevels => _levels.Length;
    }
}