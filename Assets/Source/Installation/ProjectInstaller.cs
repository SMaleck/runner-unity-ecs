using Source.Services.AudioPlayer;
using Zenject;

namespace Source.Installation
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AudioPlayerService>().AsSingle().NonLazy();
        }
    }
}
