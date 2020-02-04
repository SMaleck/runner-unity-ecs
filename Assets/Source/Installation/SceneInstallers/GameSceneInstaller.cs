using Source.Features.EntitySpawning;
using Source.Features.HelloWorld;
using Source.Initialization;
using UGF.Installation;
using UGF.Util;
using UGF.Views.Mediation;

namespace Source.Installation.SceneInstallers
{
    public class GameSceneInstaller : AbstractSceneInstaller
    {
        protected override void InstallSceneBindings()
        {
            Container.BindInterfacesAndSelfTo<GameSceneInitializer>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<ClosableViewMediator>().AsSingle().NonLazy();
            Container.BindFactory<IClosableView, ClosableViewController, ClosableViewController.Factory>();

            Container.BindPrefabFactory<HelloWorldHudView, HelloWorldHudView.Factory>();
            Container.BindPrefabFactory<HelloWorldGameView, HelloWorldGameView.Factory>();

            Container.BindInterfacesAndSelfTo<EntitySpawner>().AsSingle().NonLazy();
        }
    }
}
