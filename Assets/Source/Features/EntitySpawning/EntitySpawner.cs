using Source.Features.EntitySpawning.Factories;
using UGF.Util.UniRx;
using UnityEngine;

namespace Source.Features.EntitySpawning
{
    public class EntitySpawner : AbstractDisposable, IPlayerSpawner, IFloorSpawner, IObstacleSpawner
    {
        private readonly PlayerEntityFactory _playerEntityFactory;
        private readonly FloorTileEntityFactory _floorTileEntityFactory;
        private readonly ObstacleEntityFactory _obstacleEntityFactory;

        public EntitySpawner(
            PlayerEntityFactory playerEntityFactory,
            FloorTileEntityFactory floorTileEntityFactory,
            ObstacleEntityFactory obstacleEntityFactory)
        {
            _playerEntityFactory = playerEntityFactory;
            _floorTileEntityFactory = floorTileEntityFactory;
            _obstacleEntityFactory = obstacleEntityFactory;
        }

        public void SpawnPlayerAt(Vector3 position)
        {
            _playerEntityFactory.CreateEntityAt(position);
        }

        public void SpawnFloorTileAt(Vector3 position)
        {
            _floorTileEntityFactory.CreateEntityAt(position);
        }

        public void SpawnObstacleAt(Vector3 position)
        {
            _obstacleEntityFactory.CreateEntityAt(position);
        }
    }
}
