using DI;
using Entities.Components;
using Events;
using Events.Effects;
using Game.EventBus;
using UnityEngine;

namespace Handlers
{
    public class UltimateEventHandler : BaseHandler<UltimateEffect>
    {
        [Inject]
        private new void Construct(EventBus eventBus)
        {
            base.Construct(eventBus);
        }

        protected override void HandleEvent(UltimateEffect evt)
        {
            Debug.Log($"{evt.Source.Get<Name>().Value} is using it's ultimate!");
            EventBus.RaiseEvent(new UltimateEvent(evt.Source, evt.Target));
        }
    }
}