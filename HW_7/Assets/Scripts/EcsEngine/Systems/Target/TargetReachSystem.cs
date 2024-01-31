using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    public class TargetReachSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TargetEntity, Position, AttackRange, TargetReached>> filter;

        private readonly EcsPoolInject<Position> positionPool;

        private readonly EcsPoolInject<MeleeAttackRangeOffset> offsetPool;
        private readonly EcsPoolInject<MeleeTag> meleePool;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter.Value)
            {
                int target = filter.Pools.Inc1.Get(entity).value;
                Vector3 targetPos = positionPool.Value.Get(target).value;

                Vector3 pos = filter.Pools.Inc2.Get(entity).value;
                float attackRange = filter.Pools.Inc3.Get(entity).value;
                ref TargetReached targetReached = ref filter.Pools.Inc4.Get(entity);

                float distance = Vector3.Distance(targetPos, pos);

                if (offsetPool.Value.Has(target) && meleePool.Value.Has(entity))
                {
                    distance -= offsetPool.Value.Get(target).value;
                }
                
                targetReached.value = distance <= attackRange;
            }
        }
    }
}