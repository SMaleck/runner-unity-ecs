using Source.Initialization;
using Zenject;

namespace Source.Installation.SceneInstallers
{
    public class InitSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InitSceneInitializer>().AsSingle().NonLazy();
        }
    }
}
