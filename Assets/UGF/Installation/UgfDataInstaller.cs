using UGF.Services.Audio.Config;
using UGF.Services.Savegames.Config;
using UGF.Services.SceneManagement.Config;
using UnityEngine;
using Zenject;

namespace UGF.Installation
{
    [CreateAssetMenu(fileName = nameof(UgfDataInstaller), menuName = UgfConstants.UMenuInstallers + nameof(UgfDataInstaller))]
    public class UgfDataInstaller : ScriptableObjectInstaller<UgfDataInstaller>
    {
        [SerializeField] private SceneManagementConfig _sceneManagementConfig;
        [SerializeField] private SavegamesConfig _savegamesConfig;
        [SerializeField] private AudioServiceConfig _audioServiceConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_sceneManagementConfig);
            Container.BindInstance(_savegamesConfig);
            Container.BindInstance(_audioServiceConfig);
        }
    }
}