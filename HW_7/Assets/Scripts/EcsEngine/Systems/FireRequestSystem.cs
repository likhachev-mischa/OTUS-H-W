using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class FireRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<FireRequest, BulletWeapon>, Exc<Inactive>> filter;
        
        private readonly EcsWorldInject eventWorld = EcsWorlds.Events;
        private readonly EcsPoolInject<SpawnRequest> spawnPool = EcsWorlds.Events;
        private readonly EcsPoolInject<Position> positionPool = EcsWorlds.Events;
        private readonly EcsPoolInject<Rotation> rotationPool = EcsWorlds.Events;
        private readonly EcsPoolInject<Prefab> prefabPool = EcsWorlds.Events;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            EcsPool<BulletWeapon> weaponPool = this.filter.Pools.Inc2;
            EcsPool<FireRequest> requestPool = this.filter.Pools.Inc1;

            foreach (int entity in this.filter.Value)
            {
                BulletWeapon weapon = weaponPool.Get(entity);
                
                //Spawn bullet...
                int spawnEvent = this.eventWorld.Value.NewEntity();
                
                this.spawnPool.Value.Add(spawnEvent) = new SpawnRequest();
                this.positionPool.Value.Add(spawnEvent) = new Position {value = weapon.firePoint.position};
                this.rotationPool.Value.Add(spawnEvent) = new Rotation {value = weapon.firePoint.rotation};
                this.prefabPool.Value.Add(spawnEvent) = new Prefab {value = weapon.bulletPrefab};
                
                Debug.Log($"Pew {weapon.bulletPrefab.name}!");
                requestPool.Del(entity);
            }
        }
    }
}