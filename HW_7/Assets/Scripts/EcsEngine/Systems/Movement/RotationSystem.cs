using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public class RotationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveDirection, Rotation, UnitTag>, Exc<Inactive>> filter;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter.Value)
            {
                Vector3 direction = filter.Pools.Inc1.Get(entity).value;

                ref Rotation rotation = ref filter.Pools.Inc2.Get(entity);
                rotation.value.SetLookRotation(direction);
            }
        }
    }
}