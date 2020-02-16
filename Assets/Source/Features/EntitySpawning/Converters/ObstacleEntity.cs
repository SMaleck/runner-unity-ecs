using Unity.Entities;
using UnityEngine;

namespace Source.Features.EntitySpawning.Converters
{
    public class ObstacleEntity : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private int _damage;

        public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
        {
        }
    }
}
