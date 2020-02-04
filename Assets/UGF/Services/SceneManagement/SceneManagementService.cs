using UGF.Services.SceneManagement.LoadingScreen;
using UGF.Util.UniRx;
using UniRx;
using UnityEngine.SceneManagement;

namespace UGF.Services.SceneManagement
{
    public class SceneManagementService : AbstractDisposable, ISceneManagementService
    {
        private readonly SceneManagementModel _sceneManagementModel;
        private readonly LoadingScreenModel _loadingScreenModel;
        private readonly SerialDisposable _loadingScreenVisibilityDisposer;

        private readonly Subject<Unit> _onSceneLoadStarted;
        public IOptimizedObservable<Unit> OnSceneLoadStarted => _onSceneLoadStarted;

        public SceneManagementService(
            SceneManagementModel sceneManagementModel,
            LoadingScreenModel loadingScreenModel)
        {
            _sceneManagementModel = sceneManagementModel;
            _loadingScreenModel = loadingScreenModel;
            _loadingScreenVisibilityDisposer = new SerialDisposable().AddTo(Disposer);

            _onSceneLoadStarted = new Subject<Unit>().AddTo(Disposer);

            _loadingScreenModel.OnCloseLoadingScreenCompleted
                .Subscribe(_ => _sceneManagementModel.PublishOnSceneStarted())
                .AddTo(Disposer);
        }

        private void StartSceneLoad(string sceneName)
        {
            _loadingScreenModel.SetIsLoadingScreenVisible(true);
            _onSceneLoadStarted.OnNext(Unit.Default);

            _loadingScreenVisibilityDisposer.Disposable = _loadingScreenModel.OnOpenLoadingScreenCompleted
                .Subscribe(_ => LoadScene(sceneName));
        }

        private void LoadScene(string sceneName)
        {
            _loadingScreenVisibilityDisposer.Disposable?.Dispose();
            SceneManager.LoadSceneAsync(sceneName);
        }

        public void OnSceneInitializationCompleted()
        {
            _loadingScreenModel.SetIsLoadingScreenVisible(false);
        }

        public void ToScene(string sceneName)
        {
            StartSceneLoad(sceneName);
        }
    }
}
