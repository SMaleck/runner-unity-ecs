using Source.Features.UiScreens;
using Source.Initialization;
using UGF.Installation;
using UGF.Util;
using UGF.Views.Mediation;

namespace Source.Installation.SceneInstallers
{
    public class TitleSceneInstaller : AbstractSceneInstaller
    {
        protected override void InstallSceneBindings()
        {
            Container.BindInterfacesAndSelfTo<TitleSceneInitializer>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<ClosableViewMediator>().AsSingle().NonLazy();
            Container.BindFactory<IClosableView, ClosableViewController, ClosableViewController.Factory>();

            Container.BindPrefabFactory<TitleMenuView, TitleMenuView.Factory>();
        }
    }
}