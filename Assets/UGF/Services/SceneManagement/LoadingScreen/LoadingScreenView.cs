using DG.Tweening;
using UGF.Util.UniRx;
using UGF.Views;
using UniRx;
using UnityEngine;
using Zenject;

namespace UGF.Services.SceneManagement.LoadingScreen
{
    public class LoadingScreenView : AbstractView
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, LoadingScreenView> { }

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeSeconds;

        private LoadingScreenModel _loadingScreenModel;
        private Tween _openTween;
        private Tween _closeTween;

        [Inject]
        private void Inject(LoadingScreenModel loadingScreenModel)
        {
            _loadingScreenModel = loadingScreenModel;
        }

        public override void OnInitialize()
        {
            _canvasGroup.alpha = 0;
            var openFade = _canvasGroup.DOFade(1, _fadeSeconds);
            openFade.ForceInit();

            _openTween = DOTween.Sequence()
                .AppendCallback(Open)
                .Append(openFade)
                .AppendCallback(OnOpenCompleted)
                .SetAutoKill(false)
                .Pause()
                .AddTo(Disposer, TweenDisposalBehaviour.Rewind);

            _canvasGroup.alpha = 1;
            var closeFade = _canvasGroup.DOFade(0, _fadeSeconds);
            closeFade.ForceInit();

            _closeTween = DOTween.Sequence()
                .AppendCallback(Open)
                .Append(closeFade)
                .AppendCallback(OnCloseCompleted)
                .SetAutoKill(false)
                .Pause()
                .AddTo(Disposer, TweenDisposalBehaviour.Rewind);

            _loadingScreenModel.IsLoadingScreenVisible
                .Subscribe(UpdateVisibility)
                .AddTo(Disposer);
        }

        private void OnOpenCompleted()
        {
            _loadingScreenModel.PublishOnOpenLoadingScreenCompleted();
        }

        private void OnCloseCompleted()
        {
            _loadingScreenModel.PublishOnCloseLoadingScreenCompleted();
            SetActive(false);
        }

        private void UpdateVisibility(bool isVisible)
        {
            if (isVisible)
            {
                _openTween.Restart();
                return;
            }

            _closeTween.Restart();
        }

        private void Open()
        {
            SetActive(true);
        }
    }
}
