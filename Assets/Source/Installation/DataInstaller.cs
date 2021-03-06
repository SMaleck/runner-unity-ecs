﻿using Source.Features.Camera.Config;
using Source.Features.EntitySpawning.Config;
using Source.Installation.Config;
using Source.Services.AudioPlayer.Config;
using Source.Services.Savegames.Config;
using UnityEngine;
using Zenject;

namespace Source.Installation
{
    [CreateAssetMenu(fileName = nameof(DataInstaller), menuName = Constants.UMenuInstallers + nameof(DataInstaller))]
    public class DataInstaller : ScriptableObjectInstaller<DataInstaller>
    {
        [SerializeField] private SavegamesConfig _savegamesConfig;
        [SerializeField] private ViewPrefabConfig _viewPrefabConfig;
        [SerializeField] private AudioClipsConfig _audioClipsConfig;
        [SerializeField] private GameCameraConfig _gameCameraConfig;
        [SerializeField] private EntitySpawningConfig _entitySpawningConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_savegamesConfig);
            Container.BindInstance(_viewPrefabConfig);
            Container.BindInstance(_audioClipsConfig);
            Container.BindInstance(_gameCameraConfig);

            Container.BindInstance(_entitySpawningConfig.PlayerEntityConfig);
            Container.BindInstance(_entitySpawningConfig.FloorTileEntityConfig);
            Container.BindInstance(_entitySpawningConfig.ObstacleEntityConfig);
            Container.BindInstance(_entitySpawningConfig.CloudEntityConfig);
        }
    }
}