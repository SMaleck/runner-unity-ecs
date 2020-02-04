using Source.Features.HelloWorld;
using Source.Installation.Config;
using UGF.Initialization;
using UGF.Views.Mediation;
using Zenject;

namespace Source.Initialization
{
    public class GameSceneInitializer : AbstractSceneInitializer
    {
        [Inject] private readonly ViewPrefabConfig _viewPrefabConfig;
        [Inject] private readonly HelloWorldHudView.Factory _helloWorldHudViewFactory;
        [Inject] private readonly HelloWorldGameView.Factory _helloWorldGameViewFactory;

        public override void Initialize()
        {
            InitViews();
        }

        private void InitViews()
        {
            var helloWorldHudView = _helloWorldHudViewFactory.Create(_viewPrefabConfig.HelloWorldHudViewPrefab);
            SetupView(helloWorldHudView);

            var helloWorldGameView = _helloWorldGameViewFactory.Create(_viewPrefabConfig.HelloWorldGameViewPrefab);
            SetupClosableView(helloWorldGameView, ClosableViewType.HelloWorld);
        }
    }
}
