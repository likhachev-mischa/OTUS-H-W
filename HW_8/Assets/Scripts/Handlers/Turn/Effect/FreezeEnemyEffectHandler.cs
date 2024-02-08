using DI;
using Entities;
using Entities.Components;
using Events.Effects;
using Game.EventBus;
using Pipeline;
using UnityEngine;

namespace Handlers.Turn
{
    public class FreezeEnemyEffectHandler : BaseHandler<FreezeEnemyEffect>
    {
        private EffectStack effectStack;

        [Inject]
        private void Construct(EventBus eventBus, EffectStack effectStack)
        {
            base.Construct(eventBus);
            this.effectStack = effectStack;
        }

        protected override void HandleEvent(FreezeEnemyEffect evt)
        {
            IEntity target = evt.Target.entity;

            if (target.TryGet(out Inactive inactive))
            {
                inactive.Value = true;
                inactive.Duration = evt.Duration;
            }
            else
            {
                target.Add(new Inactive() { Value = true, Duration = evt.Duration });
            }

            Debug.LogWarning(
                $"{evt.Source.Get<Name>().Value} has frozen {evt.Target.entity.Get<Name>().Value} for {evt.Duration} turns");

            if (!effectStack.IsEmpty())
            {
                EventBus.RaiseEvent(effectStack.Pop());
            }
        }
    }
}