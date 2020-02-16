using Source.Entities.Components;
using Unity.Entities;
using UnityEngine;

namespace Source.Features.EntitySpawning.Converters
{
    public class ObstacleEntity : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private int _damage;

        public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
        {
            entityManager.AddComponentData(entity, new Damage
            {
                Value = _damage
            });
        }
    }
}
