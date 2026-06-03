using RollABall.Core.Events;
using RollABall.Domain.Models;
using RollABall.Presentation.UI.Buttons;
using RollABall.Presentation.UI.Common;
using System;

namespace RollABall.Presentation.UI.Options
{
    public class OptionsController : IDisposable
    {
        private readonly OptionsView _view;
        private readonly IEventBus _eventBus;
        private readonly AudioModel _audioModel;
        private readonly UIHoverHandler _hoverHandler;

        public OptionsController(OptionsView view, AudioModel audioModel, UIHoverableButton[] hoverableButtons, IEventBus eventBus)
        {
            _view         = view ?? throw new ArgumentNullException(nameof(view));
            _audioModel   = audioModel ?? throw new ArgumentNullException(nameof(audioModel));
            _eventBus     = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _hoverHandler = new UIHoverHandler(hoverableButtons);

            _view.MusicVolumeDownButton.onClick.AddListener(HandleMusicVolumeDown);
            _view.MusicVolumeUpButton.onClick.AddListener(HandleMusicVolumeUp);
            _view.SfxVolumeDownButton.onClick.AddListener(HandleSfxVolumeDown);
            _view.SfxVolumeUpButton.onClick.AddListener(HandleSfxVolumeUp);
            _view.BackButton.onClick.AddListener(HandleBack);

            // Suscribe eventos de navegación
            _eventBus.Subscribe<OpenOptionsRequestedEvent>(HandleOpenOptions);
            _eventBus.Subscribe<CloseOptionsRequestedEvent>(HandleCloseOptions);

            // Estado inicial
            _view.UpdateMusicVolume(_audioModel.MusicVolume);
            _view.UpdateSfxVolume(_audioModel.SfxVolume);
        }

        // ─── Handlers navegación ──────────────────────────────────────
        private void HandleOpenOptions(OpenOptionsRequestedEvent e)
        {
            _audioModel.OnMusicVolumeChanged += HandleMusicVolumeChanged;
            _audioModel.OnSfxVolumeChanged   += HandleSfxVolumeChanged;

            if (_view == null) return; // ← verifica que la view existe
            _view.UpdateMusicVolume(_audioModel.MusicVolume);
            _view.UpdateSfxVolume(_audioModel.SfxVolume);
        }

        private void HandleCloseOptions(CloseOptionsRequestedEvent e)
        {
            // Se desuscribe del modelo cuando el panel se cierra
            _audioModel.OnMusicVolumeChanged -= HandleMusicVolumeChanged;
            _audioModel.OnSfxVolumeChanged   -= HandleSfxVolumeChanged;
        }

        // ─── Handlers botones ─────────────────────────────────────────
        private void HandleMusicVolumeDown()
        {
            _eventBus.Publish(new AudioVolumeChangedEvent(
                isMusic: true,
                level: _audioModel.MusicVolume - 1
            ));
        }

        private void HandleMusicVolumeUp()
        {
            _eventBus.Publish(new AudioVolumeChangedEvent(
                isMusic: true,
                level: _audioModel.MusicVolume + 1
            ));
        }

        private void HandleSfxVolumeDown()
        {
            _eventBus.Publish(new AudioVolumeChangedEvent(
                isMusic: false,
                level: _audioModel.SfxVolume - 1
            ));
        }

        private void HandleSfxVolumeUp()
        {
            _eventBus.Publish(new AudioVolumeChangedEvent(
                isMusic: false,
                level: _audioModel.SfxVolume + 1
            ));
        }

        private void HandleBack()
        {
            _eventBus.Publish(new CloseOptionsRequestedEvent());
        }

        // ─── Handlers modelo ──────────────────────────────────────────
        private void HandleMusicVolumeChanged(int level)
        {
            _view.UpdateMusicVolume(level);
        }

        private void HandleSfxVolumeChanged(int level)
        {
            _view.UpdateSfxVolume(level);
        }

        // ─── Ciclo de vida ────────────────────────────────────────────
        public void Dispose()
        {
            // Desuscribe del modelo siempre, esté abierto o no
            _audioModel.OnMusicVolumeChanged -= HandleMusicVolumeChanged;
            _audioModel.OnSfxVolumeChanged   -= HandleSfxVolumeChanged;

            _view.MusicVolumeDownButton.onClick.RemoveListener(HandleMusicVolumeDown);
            _view.MusicVolumeUpButton.onClick.RemoveListener(HandleMusicVolumeUp);
            _view.SfxVolumeDownButton.onClick.RemoveListener(HandleSfxVolumeDown);
            _view.SfxVolumeUpButton.onClick.RemoveListener(HandleSfxVolumeUp);
            _view.BackButton.onClick.RemoveListener(HandleBack);

            _eventBus?.Unsubscribe<OpenOptionsRequestedEvent>(HandleOpenOptions);
            _eventBus?.Unsubscribe<CloseOptionsRequestedEvent>(HandleCloseOptions);

            _hoverHandler.Dispose();
        }
    }
}