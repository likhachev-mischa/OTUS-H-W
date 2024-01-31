using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public class CooldownSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackCooldown>> filter;

        private readonly EcsPoolInject<AttackEvent> attackPool;
        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            foreach (int entity in filter.Value)
            {
                ref AttackCooldown attackCooldown = ref filter.Pools.Inc1.Get(entity);
                if (attackPool.Value.Has(entity))
                {
                    attackCooldown.onCooldown = true;
                }

                if (!attackCooldown.onCooldown)
                {
                    continue;
                }

                attackCooldown.currentTimer -= deltaTime;

                if (attackCooldown.currentTimer <= 0)
                {
                    attackCooldown.currentTimer = attackCooldown.value;
                    attackCooldown.onCooldown = false;
                }
            }
        }
    }
}