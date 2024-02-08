using DI;
using Entities;
using Entities.Components;
using Events.Effects;
using Game.EventBus;
using Pipeline;
using UnityEngine;

namespace Handlers.Turn
{
    public class HealEffectHandler : BaseHandler<HealEffect>
    {
        private EffectStack effectStack;

        [Inject]
        private void Construct(EventBus eventBus, EffectStack effectStack)
        {
            base.Construct(eventBus);
            this.effectStack = effectStack;
        }

        protected override void HandleEvent(HealEffect evt)
        {
            IEntity target = evt.Target.entity;

            if (!target.TryGet(out Health health))
            {
                if (!effectStack.IsEmpty())
                {
                    EventBus.RaiseEvent(effectStack.Pop());
                }

                return;
            }

            health.Value += evt.Value;

            Debug.LogWarning(
                $"{evt.Source.Get<Name>().Value} has healed {target.Get<Name>().Value} for {evt.Value} health");

            if (!effectStack.IsEmpty())
            {
                EventBus.RaiseEvent(effectStack.Pop());
            }
        }
    }
}