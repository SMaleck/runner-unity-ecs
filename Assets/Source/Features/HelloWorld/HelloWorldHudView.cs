using TMPro;
using UGF.Services.Localization;
using UGF.Views;
using UGF.Views.Mediation;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Features.HelloWorld
{
    public class HelloWorldHudView : AbstractView, ILocalizable
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, HelloWorldHudView> { }

        [SerializeField] private TextMeshProUGUI _helloWorldText;
        [SerializeField] private Button _openButton;

        private IClosableViewMediator _closableViewMediator;

        [Inject]
        private void Inject(IClosableViewMediator closableViewMediator)
        {
            _closableViewMediator = closableViewMediator;
        }

        public override void OnInitialize()
        {
            _openButton.OnClickAsObservable()
                .Subscribe(_ => _closableViewMediator.Open(ClosableViewType.HelloWorld))
                .AddTo(Disposer);
        }

        public void Localize()
        {
            _helloWorldText.text = TextService.HelloWorld();
        }
    }
}
