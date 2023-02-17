using UnityEngine;
using Zenject;

namespace Installers
{
    public class CurtainInstaller : MonoInstaller
    {
        [SerializeField] private Curtain _prefab;
        public override void InstallBindings()
        {
            Container
                .Bind<Curtain>()
                .FromComponentsInNewPrefab(_prefab)
                .AsSingle();
        }
    }
}