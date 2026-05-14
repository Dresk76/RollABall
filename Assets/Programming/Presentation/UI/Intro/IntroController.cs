using RollABall.Core.Events;
using RollABall.Domain.Enums;
using System;
using UnityEngine;

namespace RollABall.Presentation.UI.Intro
{
    public class IntroController : IDisposable
    {
        private readonly IntroModel _model;
        private readonly IntroView _view;
        private readonly IEventBus _eventBus;

        private float _timer;
        private int _currentValue;

        public IntroController(IntroModel model, IntroView view, IEventBus eventBus, int countdownStartValue)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            _currentValue = countdownStartValue;

            _model.OnCountdownChanged += HandleCountdownChanged;
            _model.SetCountdown(_currentValue);
        }

        public void Tick()
        {
            _timer += Time.deltaTime;

            if (_timer >= 1f)
            {
                _timer = 0f;
                _currentValue--;
                _model.SetCountdown(_currentValue);

                if (_currentValue <= 0)
                {
                    _eventBus.Publish(new LoadSceneEvent(SceneType.MainMenuScene));
                }
            }
        }

        private void HandleCountdownChanged(int value)
        {
            _view.SetCountdownText(value.ToString());
        }

        public void Dispose()
        {
            _model.OnCountdownChanged -= HandleCountdownChanged;
        }
    }
}