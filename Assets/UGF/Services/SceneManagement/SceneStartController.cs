using Zenject;

namespace UGF.Services.SceneManagement
{
    public class SceneStartController : IInitializable
    {
        private readonly SceneManagementService _sceneManagementService;

        public SceneStartController(SceneManagementService sceneManagementService)
        {
            _sceneManagementService = sceneManagementService;
        }

        public void Initialize()
        {
            _sceneManagementService.OnSceneInitializationCompleted();
        }
    }
}