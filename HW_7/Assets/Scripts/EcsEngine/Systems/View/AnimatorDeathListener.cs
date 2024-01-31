using Content;
using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class AnimatorDeathListener : IEcsRunSystem
    {
        private static readonly int MainState = Animator.StringToHash("MainState");

        private readonly EcsFilterInject<Inc<AnimatorView, DeathEvent>> filter;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entity in this.filter.Value)
            {
                Animator animator = this.filter.Pools.Inc1.Get(entity).value;
                animator.SetInteger(MainState,(int)UnitAnimationState.DEAD);
            }
        }
    }
}