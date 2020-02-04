using Source.Services.SceneTransition;
using TMPro;
using UGF.Services.Localization;
using UGF.Views;
using UGF.Views.Mediation;
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
        
        private IClosableViewMediator _closableViewMediator;
        private ISceneTransitionService _sceneTransitionService;

        [Inject]
        private void Inject(
            IClosableViewMediator closableViewMediator,
            ISceneTransitionService sceneTransitionService)
        {
            _closableViewMediator = closableViewMediator;
            _sceneTransitionService = sceneTransitionService;
        }

        public override void OnInitialize()
        {
            _resetButton.OnClickAsObservable()
                .Subscribe(_ => _sceneTransitionService.ToGame())
                .AddTo(Disposer);
        }

        public void Localize()
        {
            _resetButtonText.text = TextService.Restart();
        }
    }
}
