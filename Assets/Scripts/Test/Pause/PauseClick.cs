using UnityEngine;
using NaughtyAttributes;
using Zenject;
using SmartHomeSimulation.Core.Pause;

namespace SmartHomeSimulation.Test.Pause
{
    public sealed class PauseClick : MonoBehaviour
    {
        [Inject] private PauseManager _pauseManager;

        [Button]
        private void InvokePauseProcess()
        {
            _pauseManager.SetPaused(!_pauseManager.IsPaused);
        }
    }
}
