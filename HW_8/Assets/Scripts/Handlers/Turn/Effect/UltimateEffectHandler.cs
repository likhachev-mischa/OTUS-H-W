using DI;
using Entities.Components;
using Events.Effects;
using Game.EventBus;
using Pipeline;
using UnityEngine;

namespace Handlers.Turn
{
    public sealed class UltimateEffectHandler : BaseHandler<UltimateEffect>
    {
        private EffectStack effectStack;

        [Inject]
        public void Construct(EventBus eventBus, EffectStack effectStack)
        {
            base.Construct(eventBus);
            this.effectStack = effectStack;
        }

        protected override void HandleEvent(UltimateEffect evt)
        {
            Debug.LogWarning($"{evt.Source.Get<Name>().Value} has used it's ultimate!");

            if (!effectStack.IsEmpty())
            {
                EventBus.RaiseEvent(effectStack.Pop());
            }
        }
    }
}