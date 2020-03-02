using Source.Features.Camera;
using Source.Features.DataBridge;
using Source.Features.EntitySpawning;
using Source.Features.EntitySpawning.Factories;
using Source.Features.EntitySpawning.SpawningControllers;
using Source.Features.PlayerStats;
using Source.Features.RunEnd;
using Source.Features.ScreenSize;
using Source.Features.UiHud;
using Source.Initialization;
using UGF.Installation;
using UGF.Util;
using UGF.Views.Mediation;
using UnityEngine;

namespace Source.Installation.SceneInstallers
{
    public class GameSceneInstaller : AbstractSceneInstaller
    {
        [SerializeField] private GameCamera _sceneCamera;

        protected override void InstallSceneBindings()
        {
            Blackboard.Reset();

            Container.BindInstance(_sceneCamera);

            Container.BindInterfacesAndSelfTo<GameSceneInitializer>().AsSingleNonLazy();

            Container.BindInterfacesAndSelfTo<ClosableViewMediator>().AsSingleNonLazy();
            Container.BindFactory<IClosableView, ClosableViewController, ClosableViewController.Factory>();

            Container.BindPrefabFactory<HudView, HudView.Factory>();
            Container.BindPrefabFactory<RunEndView, RunEndView.Factory>();
            Container.BindInterfacesAndSelfTo<RunEndController>().AsSingleNonLazy();
            
            Container.BindInterfacesAndSelfTo<GameCameraController>().AsSingleNonLazy();
            Container.BindInterfacesAndSelfTo<ScreenSizeModel>().AsSingleNonLazy();
            Container.BindInterfacesAndSelfTo<ScreenSizeController>().AsSingleNonLazy();
            
            // ToDo [ECS] Should this be bound here or on the Project level? How to handle EcsCleanupController if Project Level?
            Container.BindInterfacesAndSelfTo<PlayerEntityFactory>().AsSingleNonLazy();
            Container.BindInterfacesAndSelfTo<FloorTileEntityFactory>().AsSingleNonLazy();
            Container.BindInterfacesAndSelfTo<ObstacleEntityFactory>().AsSingleNonLazy();
            Container.BindInterfacesAndSelfTo<CloudEntityFactory>().AsSingleNonLazy();

            Container.BindInterfacesAndSelfTo<EcsCleanupController>().AsSingleNonLazy();
            Container.BindInterfacesAndSelfTo<PlayerSpawningController>().AsSingleNonLazy();
            Container.BindInterfacesAndSelfTo<FloorSpawningController>().AsSingleNonLazy();
            Container.BindInterfacesAndSelfTo<ObstacleSpawningController>().AsSingleNonLazy();
            Container.BindInterfacesAndSelfTo<CloudSpawningController>().AsSingleNonLazy();

            Container.BindInterfacesAndSelfTo<PlayerStatsModel>().AsSingleNonLazy();
            Container.BindInterfacesAndSelfTo<PlayerStatsController>().AsSingleNonLazy();
        }
    }
}
