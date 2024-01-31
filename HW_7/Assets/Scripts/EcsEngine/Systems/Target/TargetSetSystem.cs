using Content;
using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public class TargetSetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Team, Position, UnitTag>, Exc<TargetEntity, Inactive>> filter;

        private readonly EcsFilterInject<Inc<DamagableTag, Team, Position>, Exc<Inactive>> enemies;

        private readonly EcsPoolInject<TargetEntity> targetPool;
        private readonly EcsPoolInject<TargetReached> targetReachedPool;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter.Value)
            {
                Teams team = filter.Pools.Inc1.Get(entity).value;
                Vector3 position = filter.Pools.Inc2.Get(entity).value;

                int cachedEnemy = -1;
                float minDistance = float.MaxValue;

                foreach (int enemy in enemies.Value)
                {
                    Teams enemyTeam = enemies.Pools.Inc2.Get(enemy).value;
                    if (enemyTeam != team)
                    {
                        Vector3 enemyPosition = enemies.Pools.Inc3.Get(enemy).value;
                        float distance = Vector3.Distance(position, enemyPosition);
                        if (distance < minDistance)
                        {
                            cachedEnemy = enemy;
                            minDistance = distance;
                        }
                    }
                }

                if (cachedEnemy == -1)
                {
                    continue;
                }
                
                targetPool.Value.Add(entity) = new TargetEntity() { value = cachedEnemy };
                targetReachedPool.Value.Add(entity) = new TargetReached() { value = false };
            }
        }
    }
}