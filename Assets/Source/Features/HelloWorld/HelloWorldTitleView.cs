using TMPro;
using UGF.Services.Localization;
using UGF.Services.SceneManagement;
using UGF.Views;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Features.HelloWorld
{
    public class HelloWorldTitleView : AbstractView, ILocalizable
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, HelloWorldTitleView> { }

        [SerializeField] private TextMeshProUGUI _helloWorldText;
        [SerializeField] private Button _startButton;

        private ISceneManagementService _sceneManagementService;

        [Inject]
        private void Inject(ISceneManagementService sceneManagementService)
        {
            _sceneManagementService = sceneManagementService;
        }

        public override void OnInitialize()
        {
            _startButton.OnClickAsObservable()
                .Subscribe(_ => _sceneManagementService.ToGame())
                .AddTo(Disposer);
        }

        public void Localize()
        {
            _helloWorldText.text = TextService.HelloWorld();
        }
    }
}
