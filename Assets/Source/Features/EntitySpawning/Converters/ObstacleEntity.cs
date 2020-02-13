using Unity.Entities;
using UnityEngine;

namespace Source.Features.EntitySpawning.Converters
{
    public class ObstacleEntity : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
        {
            
        }
    }
}
