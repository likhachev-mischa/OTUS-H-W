using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class TakeDamageRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TakeDamageRequest, TargetEntity, Damage>, Exc<Inactive>> filter =
            EcsWorlds.Events;

        private readonly EcsPoolInject<TakeDamageEvent> eventPool = EcsWorlds.Events;
        private readonly EcsPoolInject<OneFrame> oneFramePool = EcsWorlds.Events;
        
        private readonly EcsPoolInject<Health> healthPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int @event in this.filter.Value)
            {
                int target = this.filter.Pools.Inc2.Get(@event).value;
                int damage = this.filter.Pools.Inc3.Get(@event).value;

                if (this.healthPool.Value.Has(target))
                {
                    ref int health = ref this.healthPool.Value.Get(target).value;
                    health = Mathf.Max(0, health - damage);
                }

                this.filter.Pools.Inc1.Del(@event);
                this.eventPool.Value.Add(@event) = new TakeDamageEvent();
                this.oneFramePool.Value.Add(@event) = new OneFrame();
            }
        }

    }
}