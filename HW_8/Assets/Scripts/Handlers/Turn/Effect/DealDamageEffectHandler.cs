using DI;
using Entities.Components;
using Events;
using Events.Effects;
using Game.EventBus;

namespace Handlers.Turn
{
    public sealed class DealDamageEffectHandler : BaseHandler<DealDamageEffect>
    {
        [Inject]
        public new void Construct(EventBus eventBus)
        {
            base.Construct(eventBus);
        }

        protected override void HandleEvent(DealDamageEffect evt)
        {
            int damage = evt.Source.Get<Damage>().Value;
            EventBus.RaiseEvent(new DealDamageEvent(evt.Source, evt.Target, damage));
        }
    }
}