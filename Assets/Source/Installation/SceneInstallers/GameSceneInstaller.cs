using Source.Features.Camera;
using Source.Features.DataBridge;
using Source.Features.EntitySpawning;
using Source.Features.EntitySpawning.Factories;
using Source.Features.EntitySpawning.SpawningControllers;
using Source.Features.Hud;
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
            Blackboard.Reset();

            Container.BindInstance(_sceneCamera);

            Container.BindInterfacesAndSelfTo<GameSceneInitializer>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<ClosableViewMediator>().AsSingle().NonLazy();
            Container.BindFactory<IClosableView, ClosableViewController, ClosableViewController.Factory>();

            Container.BindPrefabFactory<HudView, HudView.Factory>();

            Container.BindInterfacesAndSelfTo<GameCameraController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScreenSizeModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScreenSizeController>().AsSingle().NonLazy();

            // ToDo [ECS] Should this be bound here or on the Project level? How to handle EcsCleanupController if Project Level?
            Container.BindInterfacesAndSelfTo<PlayerEntityFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FloorTileEntityFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ObstacleEntityFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CloudEntityFactory>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<EcsCleanupController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerSpawningController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FloorSpawningController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ObstacleSpawningController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CloudSpawningController>().AsSingle().NonLazy();
        }
    }
}
