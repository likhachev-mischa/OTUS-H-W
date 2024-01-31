using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public class ProjectileMovementInitializationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SpawnEvent, ProjectileTag, CreatorEntity, MoveDirection, Rotation>> filter;

        private readonly EcsPoolInject<MoveDirection> moveDirectionPool;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter.Value)
            {
                int creator = filter.Pools.Inc3.Get(entity).value;
                ref MoveDirection moveDirection = ref filter.Pools.Inc4.Get(entity);
                ref Rotation rotation = ref filter.Pools.Inc5.Get(entity);

                if (moveDirectionPool.Value.Has(creator))
                {
                    moveDirection = moveDirectionPool.Value.Get(creator);
                    rotation.value = Quaternion.LookRotation(moveDirection.value, Vector3.up);
                }
            }
        }
    }
}