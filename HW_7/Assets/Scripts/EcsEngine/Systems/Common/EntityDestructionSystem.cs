using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace EcsEngine.Systems
{
    internal sealed class EntityDestructionSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Inactive>, Exc<InPool, TemplateEntityTag>> filter;

        private readonly EcsPoolInject<InPool> inPool;
        private readonly EcsCustomInject<EntityPoolRegistry> poolRegistry;

        private readonly EcsPoolInject<TargetEntity> targetPool;
        private readonly EcsPoolInject<TargetReached> targetReachedPool;

        private readonly EcsPoolInject<DeathTimeout> deathTimerPool;

        private readonly EcsCustomInject<EntityManager> entityManager;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entity in this.filter.Value)
            {
                if (deathTimerPool.Value.Has(entity))
                {
                    continue;
                }
                if (poolRegistry.Value.HasEntity(entity))
                {
                    if (targetPool.Value.Has(entity))
                    {
                        targetPool.Value.Del(entity);
                    }

                    if (targetReachedPool.Value.Has(entity))
                    {
                        targetReachedPool.Value.Del(entity);
                    }


                    poolRegistry.Value.RemoveEntity(entity);
                    inPool.Value.Add(entity);
                }
                else
                {
                    entityManager.Value.Destroy(entity);
                }

            }
        }
    }
}