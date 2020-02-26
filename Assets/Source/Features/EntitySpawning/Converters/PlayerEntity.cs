using Source.Entities.Components;
using Source.Entities.ComponentTags;
using Unity.Entities;
using UnityEngine;

namespace Source.Features.EntitySpawning.Converters
{
    public class PlayerEntity : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(
            Entity entity, 
            EntityManager entityManager, 
            GameObjectConversionSystem conversionSystem)
        {
            entityManager.AddComponent<PlayerTag>(entity);
            entityManager.AddComponent<InputDriverTag>(entity);

            entityManager.AddComponent<MoveSpeed>(entity);
            entityManager.AddComponent<MoveDirection>(entity);
            entityManager.AddComponent<TravelStats>(entity);
            entityManager.AddComponent<JumpIntent>(entity);
            entityManager.AddComponent<Health>(entity);
            entityManager.AddComponent<DamageIn>(entity);
        }
    }
}
