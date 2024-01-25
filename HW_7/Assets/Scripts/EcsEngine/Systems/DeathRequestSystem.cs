using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    internal sealed class DeathRequestSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DeathRequest>, Exc<Inactive>> filter;
        
        private EcsPoolInject<DeathEvent> eventPool;
        private EcsPoolInject<Inactive> tagPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entity in this.filter.Value)
            {
                this.filter.Pools.Inc1.Del(entity);

                this.tagPool.Value.Add(entity) = new Inactive();
                this.eventPool.Value.Add(entity) = new DeathEvent();
            }
        }
    }
}