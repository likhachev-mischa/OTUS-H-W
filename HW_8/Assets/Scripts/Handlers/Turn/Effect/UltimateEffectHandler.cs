using DI;
using Entities.Components;
using Events;
using Events.Effects;
using Game.EventBus;

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
            EventBus.RaiseEvent(new UltimateEvent(evt.Source, evt.Target));
        }
    }
}