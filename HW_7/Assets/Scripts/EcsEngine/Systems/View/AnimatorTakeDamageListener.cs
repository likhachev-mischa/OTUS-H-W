using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class AnimatorTakeDamageListener : IEcsRunSystem
    {
        private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");

        private readonly EcsFilterInject<Inc<TakeDamageEvent, TargetEntity>> filter = EcsWorlds.Events;
        
        private readonly EcsPoolInject<AnimatorView> animatorPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int @event in this.filter.Value)
            {
                int target = this.filter.Pools.Inc2.Get(@event).value;
                
                if (this.animatorPool.Value.Has(target))
                {
                    Animator animator = this.animatorPool.Value.Get(target).value;
                    animator.SetTrigger(TakeDamage);
                }
            }
        }
    }
}