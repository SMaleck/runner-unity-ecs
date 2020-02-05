using System;
using Unity.Entities;

namespace Source.Features.EntitySpawning
{
    public class EcsCleanupController : IDisposable
    {
        public void Dispose()
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            entityManager.DestroyEntity(entityManager.UniversalQuery);
        }
    }
}
