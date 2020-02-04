using UGF.Services.SceneManagement;
using UGF.Util.UniRx;
using UniRx;
using Zenject;

namespace UGF.Services.Savegames
{
    public class SavegamePersistenceScheduler : AbstractDisposable, IInitializable
    {
        private readonly ISavegamePersistenceService _savegamePersistenceService;
        private readonly ISceneManagementService _sceneManagementService;

        public SavegamePersistenceScheduler(
            ISavegamePersistenceService savegamePersistenceService,
            ISceneManagementService sceneManagementService)
        {
            _savegamePersistenceService = savegamePersistenceService;
            _sceneManagementService = sceneManagementService;
        }

        public void Initialize()
        {
            _savegamePersistenceService.Load();

            _sceneManagementService.OnSceneLoadStarted
                .Subscribe(_ => _savegamePersistenceService.EnqueueSaveRequest())
                .AddTo(Disposer);

            Observable.OnceApplicationQuit()
                .Subscribe(_ => _savegamePersistenceService.Save())
                .AddTo(Disposer);
        }
    }
}
