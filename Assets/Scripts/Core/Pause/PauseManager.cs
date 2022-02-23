using System.Collections.Generic;

namespace SmartHomeSimulation.Core.Pause
{
    public sealed class PauseManager : IPauseHandler
    {
        readonly private List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();

        public bool IsPaused { get; private set; }

        public void RegisterPauseHandler(IPauseHandler handler)
        {
            _pauseHandlers.Add(handler);
        }

        public void UnregisterPauseHandler(IPauseHandler handler)
        {
            _pauseHandlers.Remove(handler);
        }

        public void SetPaused(bool isPaused)
        {
            IsPaused = isPaused;
            foreach (IPauseHandler handler in _pauseHandlers)
            {
                handler.SetPaused(isPaused);
            }
        }
    }
}
