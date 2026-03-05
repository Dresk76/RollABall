using RollABall.Programming.Data.Enums;

namespace RollABall.Programming.Core.Events
{
    public readonly struct LoadSceneEvent
    {
        public readonly SceneType SceneToLoad;

        public LoadSceneEvent(SceneType sceneToLoad)
        {
            SceneToLoad = sceneToLoad;
        }
    }
}
