using Content;
using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public class VFXUnitSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TakeDamageEvent, TargetEntity>> takeDamageFilter =EcsWorlds.Events;

        private readonly EcsPoolInject<VFXUnitView> vfxPool;
        public void Run(IEcsSystems systems)
        {
            foreach (int @event in takeDamageFilter.Value)
            {
                int entity = takeDamageFilter.Pools.Inc2.Get(@event).value;

                if (!vfxPool.Value.Has(entity))
                {
                    continue;
                }

                UnitVFX vfx = vfxPool.Value.Get(entity).value;
                vfx.TakeDamage();
            }
        }
    }
}