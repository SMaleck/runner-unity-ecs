using Source.Entities.Components;
using Source.Entities.Components.Tags;
using Unity.Entities;
using UnityEngine;

namespace Source.Entities.EntityConverters
{
    public class PlayerEntity : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
        {
            entityManager.AddComponent<PlayerTag>(entity);
            entityManager.AddComponent<MoveSpeed>(entity);
            entityManager.AddComponent<MoveDirection>(entity);
        }
    }
}
