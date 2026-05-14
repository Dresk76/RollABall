using RollABall.Domain.Enums;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace RollABall.Infrastructure.Configuration
{
    [CreateAssetMenu(fileName = "Scene Configuration", menuName = "Scriptable Objects/Config/Scene Configuration")]
    public class SceneConfiguration : ScriptableObject
    {
        [Serializable]
        private struct SceneEntry
        {
            public SceneType SceneType;
            public string SceneName;
        }

        [SerializeField] private SceneEntry[] _scenes;
        private Dictionary<SceneType, string> _sceneMap;

        private void OnEnable()
        {
            BuildMap();
        }

        private void BuildMap()
        {
            _sceneMap = new Dictionary<SceneType, string>();

            foreach (var entry in _scenes)
            {
                _sceneMap[entry.SceneType] = entry.SceneName;
            }
        }

        public bool TryGetSceneName(SceneType sceneType, out string sceneName)
        {
            return _sceneMap.TryGetValue(sceneType, out sceneName);
        }
    }
}