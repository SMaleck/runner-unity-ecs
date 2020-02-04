using UniRx;

namespace UGF.Services.SceneManagement
{
    public interface ISceneManagementService
    {
        IOptimizedObservable<Unit> OnSceneLoadStarted { get; }

        void ToScene(string sceneName);
    }
}
