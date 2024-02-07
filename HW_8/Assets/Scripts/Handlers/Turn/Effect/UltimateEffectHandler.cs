using DI;
using Entities.Components;
using Events;
using Events.Effects;
using Game.EventBus;
using UnityEngine;

namespace Handlers.Turn
{
    public sealed class UltimateEffectHandler : BaseHandler<UltimateEffect>
    {
        [Inject]
        public new void Construct(EventBus eventBus)
        {
            base.Construct(eventBus);
        }

        protected override void HandleEvent(UltimateEffect evt)
        {
            Debug.Log($"{evt.Source.Get<Name>().Value} has used it's ultimate!");
        }
    }
}