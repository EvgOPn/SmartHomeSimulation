using Zenject;
using SmartHomeSimulation.Core.Pause;

namespace SmartHomeSimulation.Core.Installers
{
    public sealed class PauseManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PauseManager>().AsSingle().NonLazy();
        }
    }
}