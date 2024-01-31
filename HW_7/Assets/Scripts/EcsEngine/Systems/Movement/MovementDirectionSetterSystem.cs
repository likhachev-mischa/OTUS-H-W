using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Position = EcsEngine.Components.Position;

namespace EcsEngine.Systems
{
    public class MovementDirectionSetterSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveDirection, TargetEntity, Position>,Exc<Inactive>> filter;

        private readonly EcsPoolInject<Position> positionPool;

        private readonly EcsPoolInject<Rotation> rotationPool;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter.Value)
            {
                ref MoveDirection moveDirection = ref filter.Pools.Inc1.Get(entity);
                TargetEntity targetEntity = filter.Pools.Inc2.Get(entity);
                Position position = filter.Pools.Inc3.Get(entity);

                Position enemyPosition = positionPool.Value.Get(targetEntity.value);

                moveDirection.value = new Vector3(enemyPosition.value.x - position.value.x, 0,
                    enemyPosition.value.z - position.value.z).normalized;
            }
        }
    }
}