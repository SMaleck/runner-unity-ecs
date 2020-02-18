using Source.Features.DataBridge;
using Source.Features.EntitySpawning.Config;
using Source.Features.EntitySpawning.Factories;
using Source.Features.ScreenSize;
using System;
using UGF.Util.UniRx;
using UniRx;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Source.Features.EntitySpawning.SpawningControllers
{
    public class ObstacleSpawningController : AbstractDisposable, IInitializable
    {
        private readonly ObstacleEntityFactory _obstacleEntityFactory;
        private readonly ObstacleEntityConfig _obstacleEntityConfig;
        private readonly ScreenSizeController _screenSizeController;


        public ObstacleSpawningController(
            ObstacleEntityFactory obstacleEntityFactory,
            ObstacleEntityConfig obstacleEntityConfig,
            ScreenSizeController screenSizeController)
        {
            _obstacleEntityFactory = obstacleEntityFactory;
            _obstacleEntityConfig = obstacleEntityConfig;
            _screenSizeController = screenSizeController;
        }

        public void Initialize()
        {

        }
    }
}
