using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Helpers;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class BulletCollisionRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CollisionEnterRequest, BulletTag, SourceEntity, TargetEntity>> filter =
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
                int bullet = sourceEntity.value;
                
                if (!this.deathRequestPool.Value.Has(bullet))
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
                            this.damagePool.Value.Get(bullet)
                        );
                    }
                    
                    //Пометить пулю неактивной
                    this.deathRequestPool.Value.Add(bullet);
                }

                this.eventWorld.Value.DelEntity(entity);
            }
        }
    }
}