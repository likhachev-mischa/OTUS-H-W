using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveDirection, MoveSpeed, Position, MoveState>, Exc<Inactive>> filter;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;

            EcsPool<MoveDirection> directionPool = this.filter.Pools.Inc1;
            EcsPool<MoveSpeed> speedPool = this.filter.Pools.Inc2;
            EcsPool<Position> positionPool = this.filter.Pools.Inc3;

            foreach (int entity in filter.Value)
            {
                bool canMove = filter.Pools.Inc4.Get(entity).canMove;
                ref bool isMoving = ref filter.Pools.Inc4.Get(entity).isMoving;
                if (!canMove)
                {
                    isMoving = false;
                    continue;
                }
                
                MoveDirection moveDirection = directionPool.Get(entity);
                MoveSpeed moveSpeed = speedPool.Get(entity);
                ref Position position = ref positionPool.Get(entity);
                

                isMoving = moveDirection.value != Vector3.zero;

                position.value += moveDirection.value * (moveSpeed.value * deltaTime);
            }
        }
    }
}