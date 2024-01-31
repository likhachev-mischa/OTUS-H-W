using Content;
using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public class VFXBuildingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TakeDamageEvent, TargetEntity>> takeDamageFilter =EcsWorlds.Events;
        private readonly EcsFilterInject<Inc<DeathEvent, VFXBuildingView>> deathFilter;

        private readonly EcsPoolInject<VFXBuildingView> basePool;
        private readonly EcsPoolInject<HealthCondition> healthConditionPool;
        

        public void Run(IEcsSystems systems)
        {
            foreach (int @event in takeDamageFilter.Value)
            {
                int entity = takeDamageFilter.Pools.Inc2.Get(@event).value;
                if (!basePool.Value.Has(entity))
                {
                    continue;
                }
                
                BaseVFX vfx = basePool.Value.Get(entity).value;
                vfx.OnDamageTaken();

                if (healthConditionPool.Value.Has(entity))
                {
                    HealthState state = healthConditionPool.Value.Get(entity).state;
                    switch (state)
                    {
                        case HealthState.HALF:
                            vfx.OnLightDamaged();
                            break;
                        case HealthState.LOW:
                            vfx.OnHeavyDamaged();
                            break;
                    }
                }
            }

            foreach (int entity in deathFilter.Value)
            {
                BaseVFX vfx = deathFilter.Pools.Inc2.Get(entity).value;
                vfx.OnDestroyed();
            }
        }
    }
}