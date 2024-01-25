using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    internal sealed class OneFrameEventSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OneFrame>> filter = EcsWorlds.Events;
        private readonly EcsWorldInject eventWorld = EcsWorlds.Events;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int @event in this.filter.Value)
            {
                this.eventWorld.Value.DelEntity(@event);
            }
        }
    }
}