using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Helpers;

namespace EcsEngine.Systems
{
    public class MeleeHitRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MeleeHitRequest, SourceEntity, TargetEntity>> filter =
            EcsWorlds.Events;

        private readonly EcsWorldInject eventWorld = EcsWorlds.Events;

        private readonly EcsFactoryInject<TakeDamageRequest, SourceEntity, TargetEntity, Damage> takeDamageEmitter
            = EcsWorlds.Events;

        private readonly EcsPoolInject<TargetReached> targetReachedPool;
        private readonly EcsPoolInject<Damage> damagePool;
        private readonly EcsPoolInject<DeathRequest> deathRequestPool;
        private readonly EcsPoolInject<DamagableTag> damagableTagPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            EcsPool<SourceEntity> sourcePool = this.filter.Pools.Inc2;
            EcsPool<TargetEntity> targetPool = this.filter.Pools.Inc3;

            foreach (int @event in this.filter.Value)
            {
                //Удаление запроса:

                SourceEntity sourceEntity = sourcePool.Get(@event);
                int damager = sourceEntity.value;

                TargetEntity targetEntity = targetPool.Get(@event);
                int target = targetEntity.value;

                
                //перепроверка (во время анимации может поменяться цель,
                //по которой вне зависимости от расстояния пройдет атака)
                if (targetReachedPool.Value.Has(damager))
                {
                    bool isReached = targetReachedPool.Value.Get(damager).value;
                    if (!isReached)
                    {
                        continue;
                    }
                }
                
                if (this.damagableTagPool.Value.Has(target))
                {
                    //Deal damage:
                    this.takeDamageEmitter.Value.NewEntity(
                        new TakeDamageRequest(),
                        sourceEntity,
                        targetEntity,
                        this.damagePool.Value.Get(damager)
                    );
                }
                this.eventWorld.Value.DelEntity(@event);
            }
        }
    }
}