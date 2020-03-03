using Source.Services.Localization;
using Source.Services.SceneTransition;
using TMPro;
using UGF.Views;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Features.UiScreens
{
    public class TitleMenuView : AbstractView, ILocalizable
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, TitleMenuView> { }

        [Header("Visuals")]
        [SerializeField] private TextMeshProUGUI _applicationNameText;

        [Header("Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _startButtonText;

        private ISceneTransitionService _sceneTransitionService;

        [Inject]
        private void Inject(
            ISceneTransitionService sceneTransitionService)
        {
            _sceneTransitionService = sceneTransitionService;
        }

        public override void OnInitialize()
        {
            _startButton.OnClickAsObservable()
                .Subscribe(_ => _sceneTransitionService.ToGame())
                .AddTo(Disposer);

            Localize();
        }

        public void Localize()
        {
            _applicationNameText.text = TextService.ApplicationName();
            _startButtonText.text = TextService.Play();
        }
    }
}
