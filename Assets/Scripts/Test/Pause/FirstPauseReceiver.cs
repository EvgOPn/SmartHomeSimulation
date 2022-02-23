using UnityEngine;
using SmartHomeSimulation.Core.Pause;
using Zenject;

namespace SmartHomeSimulation.Test.Pause
{
    public sealed class FirstPauseReceiver : MonoBehaviour, IPauseHandler
    {
        [Inject] private PauseManager _pauseManager;

        private void Awake()
        {
            _pauseManager.RegisterPauseHandler(this);
        }

        public void SetPaused(bool isPaused)
        {
            Debug.Log($"FirstPauseReceiver: Pause = {isPaused}");
        }
    }
}
