using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public class MovementEnablerOnReachedSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TargetReached>> filter;
        private readonly EcsFilterInject<Inc<UnitTag>, Exc<Inactive, TargetReached>> toBeDisabledUnits;
        private readonly EcsPoolInject<MoveState> moveStatePool;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter.Value)
            {
                bool isReached = filter.Pools.Inc1.Get(entity).value;
                ref bool canMove = ref moveStatePool.Value.Get(entity).canMove;

                canMove = !isReached;
            }

            foreach (int entity in toBeDisabledUnits.Value)
            {
                ref bool canMove = ref moveStatePool.Value.Get(entity).canMove;
                canMove = false;
            }
        }
    }
}