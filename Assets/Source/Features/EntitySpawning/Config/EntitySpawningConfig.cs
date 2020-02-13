using UnityEngine;

namespace Source.Features.EntitySpawning.Config
{
    [CreateAssetMenu(fileName = nameof(EntitySpawningConfig), menuName = Constants.UMenuRoot + nameof(EntitySpawningConfig))]
    public class EntitySpawningConfig : ScriptableObject
    {
        [SerializeField] private PlayerEntityConfig _playerEntityConfig;
        public PlayerEntityConfig PlayerEntityConfig => _playerEntityConfig;

        [SerializeField] private FloorTileEntityConfig _floorTileEntityConfig;
        public FloorTileEntityConfig FloorTileEntityConfig => _floorTileEntityConfig;

        [SerializeField] private CloudEntityConfig _cloudEntityConfig;
        public CloudEntityConfig CloudEntityConfig => _cloudEntityConfig;

        [SerializeField] private ObstacleEntityConfig _obstacleEntityConfig;
        public ObstacleEntityConfig ObstacleEntityConfig => _obstacleEntityConfig;

    }
}
