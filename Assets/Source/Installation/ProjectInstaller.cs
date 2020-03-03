using Source.Services.AudioPlayer;
using Source.Services.Savegames;
using Source.Services.SceneTransition;
using UGF.Util.DataStorageStrategies;
using Zenject;

namespace Source.Installation
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AudioPlayerService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SceneTransitionService>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<JsonDataStorageStrategy>().AsSingle();
            Container.BindInterfacesAndSelfTo<SavegameService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SavegamePersistenceScheduler>().AsSingle();
        }
    }
}
