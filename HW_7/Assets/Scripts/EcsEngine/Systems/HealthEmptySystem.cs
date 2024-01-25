using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    internal sealed class HealthEmptySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Health>, Exc<DeathRequest, Inactive>> filter;
        
        private readonly EcsPoolInject<DeathRequest> deathPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entity in this.filter.Value)
            {
                Health health = this.filter.Pools.Inc1.Get(entity);
                if (health.value <= 0)
                {
                    this.deathPool.Value.Add(entity) = new DeathRequest();
                }
            }
        }
    }
}