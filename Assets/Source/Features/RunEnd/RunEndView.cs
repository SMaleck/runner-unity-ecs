using Source.Features.PlayerStats;
using Source.Services.SceneTransition;
using TMPro;
using UGF.Services.Localization;
using UGF.Views;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Features.RunEnd
{
    public class RunEndView : AbstractView, ILocalizable
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, RunEndView> { }

        [SerializeField] private TextMeshProUGUI _titleText;

        [Header("Distance")]
        [SerializeField] private TextMeshProUGUI _distanceLabelText;
        [SerializeField] private TextMeshProUGUI _distanceValueText;

        [Header("Best Distance")]
        [SerializeField] private TextMeshProUGUI _bestDistanceLabelText;
        [SerializeField] private TextMeshProUGUI _bestDistanceValueText;

        [Header("Buttons")]
        [SerializeField] private Button _restartButton;
        [SerializeField] private TextMeshProUGUI _restartButtonText;

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
            _restartButton.OnClickAsObservable()
                .Subscribe(_ => _sceneTransitionService.ToGame())
                .AddTo(Disposer);

            _playerStatsModel.DistanceUnits
                .Subscribe(distance => _distanceValueText.text = TextService.AmountMeters(distance))
                .AddTo(Disposer);

            _playerStatsModel.BestDistanceUnits
                .Subscribe(bestDistance => _bestDistanceValueText.text = TextService.AmountMeters(bestDistance))
                .AddTo(Disposer);

            Localize();
        }

        public void Localize()
        {
            _titleText.text = TextService.YouDied();
            _distanceLabelText.text = TextService.Distance();
            _bestDistanceLabelText.text = TextService.BestDistance();
            _restartButtonText.text = TextService.Restart();
        }
    }
}
