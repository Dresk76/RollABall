using UnityEngine;

namespace RollABall.Presentation.UI.Victory
{
    public class ConfettiController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _confettiLeft;
        [SerializeField] private ParticleSystem _confettiRight;

        private void OnValidate()
        {
            Debug.Assert(_confettiLeft != null, nameof(_confettiLeft));
            Debug.Assert(_confettiRight != null, nameof(_confettiRight));
        }

        public void Play()
        {
            if (_confettiLeft == null || _confettiRight == null) return;
            _confettiLeft.Play();
            _confettiRight.Play();
        }

        public void Stop()
        {
            if (_confettiLeft == null || _confettiRight == null) return;
            _confettiLeft.Stop();
            _confettiRight.Stop();
        }
    }
}