using UI.Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class WindowServiceInstaller : MonoInstaller
    {
        [SerializeField] private WindowService _prefab;
        public override void InstallBindings()
        {
            WindowService instance = Container.InstantiatePrefabForComponent<WindowService>(_prefab);
            
            Container
                .Bind<IWindowService>()
                .To<WindowService>()
                .FromInstance(instance)
                .AsSingle();
        }
    }
}