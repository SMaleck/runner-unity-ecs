using UGF.Util.UniRx;
using Unity.Entities;

namespace Source.Features.EntitySpawning.Factories
{
    public abstract class AbstractEntityFactory : AbstractDisposable
    {
        protected EntityManager EntityManager => World.DefaultGameObjectInjectionWorld.EntityManager;
    }
}
