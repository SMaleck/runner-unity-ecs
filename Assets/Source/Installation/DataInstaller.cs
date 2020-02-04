using Source.Entities.Config;
using Source.Installation.Config;
using Source.Services.AudioPlayer.Config;
using UnityEngine;
using Zenject;

namespace Source.Installation
{
    [CreateAssetMenu(fileName = nameof(DataInstaller), menuName = Constants.UMenuInstallers + nameof(DataInstaller))]
    public class DataInstaller : ScriptableObjectInstaller<DataInstaller>
    {
        [SerializeField] private EntityConfig _entityConfig;
        [SerializeField] private ViewPrefabConfig _viewPrefabConfig;
        [SerializeField] private AudioClipsConfig _audioClipsConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_entityConfig);
            Container.BindInstance(_viewPrefabConfig);
            Container.BindInstance(_audioClipsConfig);
        }
    }
}