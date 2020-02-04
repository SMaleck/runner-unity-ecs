using Source.Services.SceneTransition;
using TMPro;
using UGF.Services.Localization;
using UGF.Views;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Features.Hud
{
    public class HudView : AbstractView, ILocalizable
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, HudView> { }

        [Header("Stats")]
        [SerializeField] private TextMeshProUGUI _distanceText;

        [Header("Buttons")]
        [SerializeField] private Button _resetButton;
        [SerializeField] private TextMeshProUGUI _resetButtonText;

        private ISceneTransitionService _sceneTransitionService;

        [Inject]
        private void Inject(
            ISceneTransitionService sceneTransitionService)
        {
            _sceneTransitionService = sceneTransitionService;
        }

        public override void OnInitialize()
        {
            _resetButton.OnClickAsObservable()
                .Subscribe(_ => _sceneTransitionService.ToGame())
                .AddTo(Disposer);

            Localize();
        }

        public void Localize()
        {
            _resetButtonText.text = TextService.Restart();
        }
    }
}
