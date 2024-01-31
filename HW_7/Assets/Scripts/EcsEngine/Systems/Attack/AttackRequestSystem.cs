using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public class AttackRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TargetReached, AttackCooldown>,Exc<Inactive>> filter;

        private readonly EcsPoolInject<AttackRequest> attackRequestPool;
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter.Value)
            {
                bool isReached = filter.Pools.Inc1.Get(entity).value;
                if (!isReached)
                {
                    continue;
                }

                bool isOnCooldown = filter.Pools.Inc2.Get(entity).onCooldown;
                if (isOnCooldown)
                {
                    continue;
                }

                attackRequestPool.Value.Add(entity);

            }
        }
    }
}