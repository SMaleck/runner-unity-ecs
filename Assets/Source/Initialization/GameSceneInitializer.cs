using Source.Features.UiHud;
using Source.Installation.Config;
using UGF.Initialization;
using Zenject;

namespace Source.Initialization
{
    public class GameSceneInitializer : AbstractSceneInitializer
    {
        [Inject] private readonly ViewPrefabConfig _viewPrefabConfig;
        [Inject] private readonly HudView.Factory _hudViewFactory;

        public override void Initialize()
        {
            InitViews();
        }

        private void InitViews()
        {
            var hudView = _hudViewFactory.Create(_viewPrefabConfig.HudViewPrefab);
            SetupView(hudView);
        }
    }
}
