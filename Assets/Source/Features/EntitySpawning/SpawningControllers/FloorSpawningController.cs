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
    public class FloorSpawningController : AbstractDisposable, IInitializable
    {
        private readonly FloorTileEntityFactory _floorTileEntityFactory;
        private readonly FloorTileEntityConfig _floorTileEntityConfig;
        private readonly ScreenSizeModel _screenSizeModel;

        private readonly Vector3 _floorTileSize;
        private float FloorTileHalfSizeX => _floorTileSize.x / 2f;
        private float FloorTileHalfSizeY => _floorTileSize.y / 2f;

        private float3 _lastFloorTileSpawnPosition;
        private float3 _lastRecordedPlayerPosition;

        public FloorSpawningController(
            FloorTileEntityFactory floorTileEntityFactory,
            FloorTileEntityConfig floorTileEntityConfig,
            ScreenSizeModel screenSizeModel)
        {
            _floorTileEntityFactory = floorTileEntityFactory;
            _floorTileEntityConfig = floorTileEntityConfig;
            _screenSizeModel = screenSizeModel;

            _floorTileSize = _floorTileEntityConfig
                .EntityMesh
                .bounds
                .size;
        }

        public void Initialize()
        {
            FillFloor();

            Blackboard.TryGet(
                BlackboardEntryId.PlayerSpawnPosition,
                out _lastRecordedPlayerPosition);

            Observable.EveryUpdate()
                .Subscribe(_ => OnUpdate())
                .AddTo(Disposer);
        }

        private void OnUpdate()
        {
            if (!Blackboard.TryGet(BlackboardEntryId.PlayerPosition, out float3 position))
            {
                return;
            }

            var distanceTraveled = Math.Abs(position.x - _lastRecordedPlayerPosition.x);
            if (distanceTraveled >= _floorTileSize.x)
            {
                SpawnNext();
                _lastRecordedPlayerPosition = position;
            }
        }

        private void FillFloor()
        {
            var leftBottomCorner = _screenSizeModel.GetCurrentLeftBottomCorner();

            var startPosition = new float3(
                leftBottomCorner.x + FloorTileHalfSizeX,
                leftBottomCorner.y + FloorTileHalfSizeY,
                0);

            var minimumTileCount = Math.Ceiling(_screenSizeModel.WidthUnits / _floorTileSize.x);
            var paddedTileCount = (int)minimumTileCount + 1;

            UGF.Logger.Log($"Creating {paddedTileCount} floor tiles at start");

            for (var i = 0; i < paddedTileCount; i++)
            {
                var sizeOffset = i * _floorTileSize.x;

                _lastFloorTileSpawnPosition = new Vector3(
                    startPosition.x + sizeOffset,
                    startPosition.y,
                    0);

                _floorTileEntityFactory.CreateEntityAt(_lastFloorTileSpawnPosition);
            }
        }

        private void SpawnNext()
        {
            var spawnPosition = new Vector3(
                _lastFloorTileSpawnPosition.x + _floorTileSize.x,
                _lastFloorTileSpawnPosition.y,
                0);

            _floorTileEntityFactory.CreateEntityAt(spawnPosition);
            _lastFloorTileSpawnPosition = spawnPosition;
        }
    }
}
