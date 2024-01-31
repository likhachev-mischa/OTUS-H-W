using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Helpers;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class ProjectileCollisionRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CollisionEnterRequest, ProjectileTag, SourceEntity, TargetEntity>> filter =
            EcsWorlds.Events;

        private readonly EcsWorldInject eventWorld = EcsWorlds.Events;
        private readonly EcsFactoryInject<TakeDamageRequest, SourceEntity, TargetEntity, Damage> takeDamageEmitter 
            = EcsWorlds.Events;

        private readonly EcsPoolInject<Damage> damagePool;
        private readonly EcsPoolInject<DeathRequest> deathRequestPool;
        private readonly EcsPoolInject<DamagableTag> damagableTagPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            EcsPool<SourceEntity> sourcePool = this.filter.Pools.Inc3;
            EcsPool<TargetEntity> targetPool = this.filter.Pools.Inc4;
            
            foreach (int entity in this.filter.Value)
            {
                //Удаление запроса:
                
                SourceEntity sourceEntity = sourcePool.Get(entity);
                int projectile = sourceEntity.value;

                if (!this.deathRequestPool.Value.Has(projectile))
                {
                    TargetEntity targetEntity = targetPool.Get(entity);
                    int target = targetEntity.value;
                
                    if (this.damagableTagPool.Value.Has(target))
                    {
                        //Deal damage:
                        this.takeDamageEmitter.Value.NewEntity(
                            new TakeDamageRequest(),
                            sourceEntity,
                            targetEntity,
                            this.damagePool.Value.Get(projectile)
                        );
                    }
                    
                    this.deathRequestPool.Value.Add(projectile) = new DeathRequest();
                }

                this.eventWorld.Value.DelEntity(entity);
            }
        }
    }
}