using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public class AnimatorAttackListener : IEcsRunSystem
    {
        private static readonly int attack = Animator.StringToHash("Attack");
        private readonly EcsFilterInject<Inc<AttackEvent, AnimatorView>> filter;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter.Value)
            {
                ref Animator animator = ref filter.Pools.Inc2.Get(entity).value;

                animator.SetTrigger(attack);
            }
        }
    }
}