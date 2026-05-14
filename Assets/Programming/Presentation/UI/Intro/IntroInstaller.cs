using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using UnityEngine;

namespace RollABall.Presentation.UI.Intro
{
    public class IntroInstaller : MonoBehaviour, ISceneInitializable
    {
        [SerializeField] private IntroView _view;
        [SerializeField] private int _countdownStartValue = 3;

        private IntroController _controller;

        private void OnValidate()
        {
            Debug.Assert(_view != null, nameof(_view));
        }

        public void Initialize(IEventBus eventBus)
        {
            IntroModel model = new();
            _controller = new IntroController(model, _view, eventBus, _countdownStartValue);
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