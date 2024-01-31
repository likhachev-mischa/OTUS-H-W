using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public class AttackEventSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackRequest>> filter;
        private readonly EcsPoolInject<AttackEvent> attackPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter.Value)
            {
                filter.Pools.Inc1.Del(entity);

                attackPool.Value.Add(entity);
            }
        }
    }
}