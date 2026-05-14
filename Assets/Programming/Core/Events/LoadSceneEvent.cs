using RollABall.Domain.Enums;

namespace RollABall.Core.Events
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