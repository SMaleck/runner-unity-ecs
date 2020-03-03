using Source.Features.PlayerStats;
using Source.Services.Localization;
using Source.Services.SceneTransition;
using TMPro;
using UGF.Views;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Features.UiHud
{
    public class HudView : AbstractView, ILocalizable
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, HudView> { }

        [Header("Stats")]
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _distanceText;

        [Header("Buttons")]
        [SerializeField] private Button _resetButton;
        [SerializeField] private TextMeshProUGUI _resetButtonText;

        private ISceneTransitionService _sceneTransitionService;
        private IPlayerStatsModel _playerStatsModel;

        [Inject]
        private void Inject(
            ISceneTransitionService sceneTransitionService,
            IPlayerStatsModel playerStatsModel)
        {
            _sceneTransitionService = sceneTransitionService;
            _playerStatsModel = playerStatsModel;
        }

        public override void OnInitialize()
        {
            _resetButton.OnClickAsObservable()
                .Subscribe(_ => _sceneTransitionService.ToGame())
                .AddTo(Disposer);

            _playerStatsModel.Health
                .Subscribe(health => _healthText.text = health.ToString())
                .AddTo(Disposer);

            _playerStatsModel.DistanceUnits
                .Subscribe(distanceUnits => _distanceText.text = TextService.AmountMeters(distanceUnits))
                .AddTo(Disposer);

            Localize();
        }

        public void Localize()
        {
            _resetButtonText.text = TextService.Restart();
        }
    }
}
