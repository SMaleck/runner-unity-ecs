using Source.Features.Hud;
using Source.Features.UiScreens;
using UnityEngine;

namespace Source.Installation.Config
{
    [CreateAssetMenu(fileName = nameof(ViewPrefabConfig), menuName = Constants.UMenuRoot + nameof(ViewPrefabConfig))]
    public class ViewPrefabConfig : ScriptableObject
    {
        [SerializeField] private TitleMenuView _titleMenuViewPrefab;
        public TitleMenuView TitleMenuViewPrefab => _titleMenuViewPrefab;

        [SerializeField] private HudView _hudViewPrefab;
        public HudView HudViewPrefab => _hudViewPrefab;
    }
}
