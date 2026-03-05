using System;
using RollABall.Programming.Core.Events;
using RollABall.Programming.Data.Enums;
using UnityEngine;

namespace RollABall.Programming.UI.Intro
{
    public class IntroController : IDisposable
    {
        private readonly IntroModel _model;
        private readonly IntroView _view;
        private readonly IEventBus _eventBus;

        private float _timer;
        private int _currentValue = 3;

        public IntroController(IntroModel model, IntroView view, IEventBus eventBus)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));;
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));;

            _model.OnCountdownChanged += HandleCountdownChanged;
            _model.SetCountdown(_currentValue);
        }

        public void Tick()
        {
            if (_timer > _currentValue) return;

            _timer += Time.deltaTime;

            if (_timer >= 1f)
            {
                _timer = 0f;
                _currentValue--;

                HandleCountdownChanged(_currentValue);

                if (_currentValue <= 0f)
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
