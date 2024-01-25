using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class SpawnRequestSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject eventWorld = EcsWorlds.Events;
        private readonly EcsFilterInject<Inc<SpawnRequest, Position, Rotation, Prefab>> filter = EcsWorlds.Events;
        
        private readonly EcsCustomInject<EntityManager> entityManager;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int @event in this.filter.Value)
            {
                Vector3 position = this.filter.Pools.Inc2.Get(@event).value;
                Quaternion rotation = this.filter.Pools.Inc3.Get(@event).value;
                Entity prefab = this.filter.Pools.Inc4.Get(@event).value;
                
                this.entityManager.Value.Create(prefab, position, rotation);
                
                this.eventWorld.Value.DelEntity(@event);
            }
        }
    }
}