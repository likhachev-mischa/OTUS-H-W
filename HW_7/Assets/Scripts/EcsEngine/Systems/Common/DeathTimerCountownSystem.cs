using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public class DeathTimerCountdownSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathEvent,DespawnTime>,Exc<DeathTimeout>> filter;
        private readonly EcsFilterInject<Inc<DeathTimeout>> timerFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter.Value)
            {
                float time = filter.Pools.Inc2.Get(entity).value;
                timerFilter.Pools.Inc1.Add(entity) = new DeathTimeout(){value = time};
            }

            float deltaTime = Time.deltaTime;
            
            foreach (int entity in timerFilter.Value)
            {
                ref float time = ref timerFilter.Pools.Inc1.Get(entity).value;
                time -= deltaTime;
                if (time <= 0)
                {
                    timerFilter.Pools.Inc1.Del(entity);
                }
                
            }
        }
    }
}