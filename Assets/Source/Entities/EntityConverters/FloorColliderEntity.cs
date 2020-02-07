using Source.Entities.Components;
using Unity.Entities;
using UnityEngine;

namespace Source.Entities.EntityConverters
{
    public class FloorColliderEntity : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
        {
            entityManager.AddComponent<FollowEntity>(entity);
        }
    }
}
