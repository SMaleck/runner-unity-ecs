using UniRx;

namespace UGF.Services.SceneManagement
{
    public interface ISceneManagementService
    {
        IOptimizedObservable<Unit> OnSceneLoadStarted { get; }

        void ToTitle();
        void ToGame();
    }
}
