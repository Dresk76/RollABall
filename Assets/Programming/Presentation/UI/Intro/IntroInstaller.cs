using RollABall.Programming.Core.Events;
using RollABall.Programming.Core.Interfaces;
using UnityEngine;

namespace RollABall.Programming.UI.Intro
{
    public class IntroInstaller : MonoBehaviour, ISceneInitializable
    {
        [SerializeField] private IntroView _view;
        private IntroController _controller;

        void OnValidate()
        {
            Debug.Assert(_view != null, nameof(_view));
        }

        public void Initialize(IEventBus eventBus)
        {
            IntroModel model = new();
            _controller = new IntroController(model, _view, eventBus);
        }

        private void Update()
        {
            _controller?.Tick();
        }

        private void OnDestroy()
        {
            _controller?.Dispose();
        }
    }
}
