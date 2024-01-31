using Content;
using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace EcsEngine.Systems
{
    public class HealthConditionUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TakeDamageEvent, TargetEntity>> takeDamageFilter =EcsWorlds.Events;

        private readonly EcsPoolInject<HealthCondition> healthConditionPool;

        private readonly EcsPoolInject<Health> healthPool;

        public void Run(IEcsSystems systems)
        {
            foreach (int @event in takeDamageFilter.Value)
            {
                int entity = takeDamageFilter.Pools.Inc2.Get(@event).value;
                if (!healthConditionPool.Value.Has(entity))
                {
                    continue;
                    
                }

                ref HealthState state = ref healthConditionPool.Value.Get(entity).state;
                int maxHealth = healthConditionPool.Value.Get(entity).maxHealth;
                int health = healthPool.Value.Get(entity).value;

                float difference = (float)health / maxHealth;

                state = difference switch
                {
                    <= 1f and > 0.5f => HealthState.FULL,
                    <= 0.5f and > 0.2f => HealthState.HALF,
                    <= 0.2f and > 0f => HealthState.LOW,
                    _ => HealthState.NONE
                };
            }
        }
    }
}