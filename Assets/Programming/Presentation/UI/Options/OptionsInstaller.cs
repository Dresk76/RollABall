using RollABall.Core.Events;
using RollABall.Core.Interfaces;
using RollABall.Core.Managers;
using RollABall.Presentation.UI.Buttons;
using UnityEngine;

namespace RollABall.Presentation.UI.Options
{
    public class OptionsInstaller : MonoBehaviour, ISceneInitializable
    {
        [SerializeField] private OptionsView _view;
        [SerializeField] private UIHoverableButton[] _hoverableButtons;

        private OptionsController _controller;

        private void OnValidate()
        {
            Debug.Assert(_view != null, nameof(_view));
        }

        public void Initialize(IEventBus eventBus)
        {
            if (AudioManager.Instance == null)
            {
                Debug.LogError("AudioManager no encontrado. Verifica que está en el Bootstrapper.");
                return;
            }

            _controller?.Dispose(); // ← limpia el anterior
            _controller = new OptionsController(
                _view,
                AudioManager.Instance.GetModel(),
                _hoverableButtons,
                eventBus
            );
        }

        private void OnDestroy()
        {
            _controller?.Dispose(); // ← limpia cuando la escena se destruye
        }
    }
}