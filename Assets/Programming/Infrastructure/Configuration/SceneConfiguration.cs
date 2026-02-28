using RollABall.Programming.Data.Enums;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace RollABall.Programming.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Scene Configuration", menuName = "Scriptable Objects/Scene Configuration")]
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

        // _sceneMap.TryGetValue(SceneType.MainMenu, out string menuName);
        public bool TryGetSceneName(SceneType sceneType, out string sceneName)
        {
            return _sceneMap.TryGetValue(sceneType, out sceneName);
        }
    }
}
