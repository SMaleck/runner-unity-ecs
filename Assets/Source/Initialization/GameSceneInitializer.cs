using Source.Features.RunEnd;
using Source.Features.UiHud;
using Source.Installation.Config;
using UGF.Initialization;
using UGF.Views.Mediation;
using Zenject;

namespace Source.Initialization
{
    public class GameSceneInitializer : AbstractSceneInitializer
    {
        [Inject] private readonly ViewPrefabConfig _viewPrefabConfig;
        [Inject] private readonly HudView.Factory _hudViewFactory;
        [Inject] private readonly RunEndView.Factory _runEndFactory;

        public override void Initialize()
        {
            InitViews();
        }

        private void InitViews()
        {
            var hudView = _hudViewFactory.Create(_viewPrefabConfig.HudViewPrefab);
            SetupView(hudView);

            var runEndView = _runEndFactory.Create(_viewPrefabConfig.RunEndViewPrefab);
            SetupClosableView(runEndView, ClosableViewType.RunEnd);
        }
    }
}
