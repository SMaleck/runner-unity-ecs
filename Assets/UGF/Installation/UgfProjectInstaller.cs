using UGF.Initialization;
using UGF.Services.Audio;
using UGF.Services.Audio.Config;
using UGF.Services.Savegames;
using UGF.Services.SceneManagement;
using UGF.Services.SceneManagement.LoadingScreen;
using UGF.Util;
using UGF.Util.DataStorageStrategies;
using Zenject;

namespace UGF.Installation
{
    public class UgfProjectInstaller : MonoInstaller
    {
        [Inject] private AudioServiceConfig _audioServiceConfig;

        public override void InstallBindings()
        {
            Container.BindExecutionOrder<ISceneInitializer>(998);
            Container.BindInterfacesAndSelfTo<UgfProjectInitializer>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<JsonDataStorageStrategy>().AsSingle();
            Container.BindInterfacesAndSelfTo<SavegameService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SavegamePersistenceScheduler>().AsSingle();

            Container.BindInterfacesAndSelfTo<SceneManagementService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneManagementModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingScreenModel>().AsSingle();
            Container.BindPrefabFactory<LoadingScreenView, LoadingScreenView.Factory>();

            Container.BindMemoryPool<AudioSourceItem, AudioSourceItem.Pool>()
                .FromComponentInNewPrefab(_audioServiceConfig.AudioSourcePrefab)
                .UnderTransformGroup("Audio");

            Container.BindInterfacesAndSelfTo<AudioService>().AsSingle().NonLazy();
        }
    }
}
