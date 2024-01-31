using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace EcsEngine.Systems
{
    public class ObjectSpawnPoolSystem<T> : IEcsRunSystem where T : struct
    {
        private readonly EcsWorldInject eventWorld = EcsWorlds.Events;

        private readonly EcsFilterInject<Inc<SpawnRequest, Position, Rotation, Prefab, T>> spawnFilter =
            EcsWorlds.Events;

        private readonly EcsPoolInject<SpawnEvent> spawnEvent;

        private readonly EcsPoolInject<Inactive> inactivePool;
        private readonly EcsPoolInject<InPool> inPool;

        private readonly EcsCustomInject<EntityPoolRegistry> poolRegistry;

        private readonly EcsPoolInject<Position> positionPool;
        private readonly EcsPoolInject<Rotation> rotationPool;

        private readonly EcsPoolInject<T> tComponentPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int @event in this.spawnFilter.Value)
            {
                Vector3 position = this.spawnFilter.Pools.Inc2.Get(@event).value;
                Quaternion rotation = this.spawnFilter.Pools.Inc3.Get(@event).value;
                Entity prefab = this.spawnFilter.Pools.Inc4.Get(@event).value;

                Entity entity = poolRegistry.Value.SpawnEntity(prefab, position, rotation);
                int id = entity.Id;

                ref Position prevPosition = ref positionPool.Value.Get(id);
                prevPosition.value = position;

                ref Rotation prevRotation = ref rotationPool.Value.Get(id);
                prevRotation.value = rotation;

                T initialTComponent = spawnFilter.Pools.Inc5.Get(@event);

                ref T tComponent = ref tComponentPool.Value.Get(id);
                tComponent = initialTComponent;

                if (inactivePool.Value.Has(id))
                {
                    inactivePool.Value.Del(id);
                }

                if (inPool.Value.Has(id))
                {
                    inPool.Value.Del(id);
                }

                this.eventWorld.Value.DelEntity(@event);
                spawnEvent.Value.Add(id);
            }
        }
    }
}