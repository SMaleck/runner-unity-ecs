using UGF.Services.SceneManagement;

namespace Source.Services.SceneTransition
{
    public class SceneTransitionService : ISceneTransitionService
    {
        private readonly ISceneManagementService _sceneManagementService;

        public SceneTransitionService(ISceneManagementService sceneManagementService)
        {
            _sceneManagementService = sceneManagementService;
        }

        public void ToTitle()
        {
            _sceneManagementService.ToScene(Scenes.TitleScene.ToString());
        }

        public void ToGame()
        {
            _sceneManagementService.ToScene(Scenes.GameScene.ToString());
        }
    }
}
