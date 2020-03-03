using UGF.Services.Audio.Config;
using UGF.Services.SceneManagement.Config;
using UnityEngine;
using Zenject;

namespace UGF.Installation
{
    [CreateAssetMenu(fileName = nameof(UgfDataInstaller), menuName = UgfConstants.UMenuInstallers + nameof(UgfDataInstaller))]
    public class UgfDataInstaller : ScriptableObjectInstaller<UgfDataInstaller>
    {
        [SerializeField] private SceneManagementConfig _sceneManagementConfig;
        [SerializeField] private AudioServiceConfig _audioServiceConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_sceneManagementConfig);
            Container.BindInstance(_audioServiceConfig);
        }
    }
}