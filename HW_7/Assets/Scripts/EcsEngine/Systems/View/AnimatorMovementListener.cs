using Content;
using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public class AnimatorMovementListener : IEcsRunSystem
    {
        private static readonly int MainState = Animator.StringToHash("MainState");
        private readonly EcsFilterInject<Inc<MoveState, AnimatorView>, Exc<Inactive>> filter;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter.Value)
            {
                ref Animator animator = ref filter.Pools.Inc2.Get(entity).value;
                MoveState moveState = filter.Pools.Inc1.Get(entity);

                if (!moveState.canMove||!moveState.isMoving)
                {
                    animator.SetInteger(MainState,(int)UnitAnimationState.IDLE);
                    continue;
                }

                animator.SetInteger(MainState,(int)UnitAnimationState.MOVING);
                
            }
        }
    }
}