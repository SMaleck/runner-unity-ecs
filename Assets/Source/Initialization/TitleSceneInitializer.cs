using Source.Features.UiScreens;
using Source.Installation.Config;
using UGF.Initialization;
using Zenject;

namespace Source.Initialization
{
    public class TitleSceneInitializer : AbstractSceneInitializer
    {
        [Inject] private readonly ViewPrefabConfig _viewPrefabConfig;
        [Inject] private readonly TitleMenuView.Factory _titleMenuViewFactory;

        public override void Initialize()
        {
            var titleMenuView = _titleMenuViewFactory.Create(_viewPrefabConfig.TitleMenuViewPrefab);
            SetupView(titleMenuView);
        }
    }
}
