using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public class TargetResetOnDeathSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathEvent,Team>> diedEntities;

        private readonly EcsFilterInject<Inc<TargetEntity, UnitTag>, Exc<Inactive>> entities;

        private readonly EcsPoolInject<TargetReached> targetReachedPool;

        public void Run(IEcsSystems systems)
        {
            foreach (int deadTarget in diedEntities.Value)
            {
                foreach (int entity in entities.Value)
                {
                    int target = entities.Pools.Inc1.Get(entity).value;
                    if (target != deadTarget)
                    {
                        continue;
                    }

                    if (targetReachedPool.Value.Has(entity))
                    {
                        targetReachedPool.Value.Del(entity);
                    }

                    if (targetReachedPool.Value.Has(deadTarget))
                    {
                        targetReachedPool.Value.Del(deadTarget);
                    }

                    if (entities.Pools.Inc1.Has(deadTarget))
                    {
                        entities.Pools.Inc1.Del(deadTarget);
                    }

                    entities.Pools.Inc1.Del(entity);
                }
            }
        }
    }
}