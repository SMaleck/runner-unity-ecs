using Source.Features.EntitySpawning;
using Source.Features.HelloWorld;
using Source.Features.ScreenSize;
using Source.Initialization;
using UGF.Installation;
using UGF.Util;
using UGF.Views.Mediation;
using UnityEngine;

namespace Source.Installation.SceneInstallers
{
    public class GameSceneInstaller : AbstractSceneInstaller
    {
        [SerializeField] private UnityEngine.Camera _sceneCamera;

        protected override void InstallSceneBindings()
        {
            Container.BindInstance(_sceneCamera);

            Container.BindInterfacesAndSelfTo<GameSceneInitializer>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<ClosableViewMediator>().AsSingle().NonLazy();
            Container.BindFactory<IClosableView, ClosableViewController, ClosableViewController.Factory>();

            Container.BindPrefabFactory<HelloWorldHudView, HelloWorldHudView.Factory>();
            Container.BindPrefabFactory<HelloWorldGameView, HelloWorldGameView.Factory>();

            Container.BindInterfacesAndSelfTo<ScreenSizeModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScreenSizeController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<EntitySpawner>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FloorSpawningController>().AsSingle().NonLazy();
        }
    }
}
