using DI;
using Entities.Components;
using Events;
using Events.Effects;
using Game.EventBus;

namespace Handlers.Turn
{
    public class ReceiveDamageEffectHandler : BaseHandler<ReceiveDamageEffect>
    {
        [Inject]
        public new void Construct(EventBus eventBus)
        {
            base.Construct(eventBus);
        }

        protected override void HandleEvent(ReceiveDamageEffect evt)
        {
            int damage = evt.Target.entity.Get<Health>().ReceivedDamage;
            EventBus.RaiseEvent(new ReceiveDamageEvent(evt.Source,evt.Target,damage));
        }
    }
}