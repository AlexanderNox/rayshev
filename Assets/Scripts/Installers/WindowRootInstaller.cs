using UI;
using Zenject;

namespace Installers
{
    public class WindowRootInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            WindowRoot instance = Container.InstantiateComponentOnNewGameObject<WindowRoot>();

            Container
                .Bind<WindowRoot>()
                .FromInstance(instance)
                .AsSingle();
        }
    }
}