using UGF.Initialization;
using UGF.Services.SceneManagement;
using Zenject;

namespace Source.Initialization
{
    public class InitSceneInitializer : ISceneInitializer
    {
        [Inject] private readonly ISceneManagementService _sceneManagementService;

        public void Initialize()
        {
            _sceneManagementService.ToTitle();
        }
    }
}
