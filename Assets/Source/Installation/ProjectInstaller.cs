using Source.Services.AudioPlayer;
using Source.Services.SceneTransition;
using Zenject;

namespace Source.Installation
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AudioPlayerService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SceneTransitionService>().AsSingle().NonLazy();
        }
    }
}
