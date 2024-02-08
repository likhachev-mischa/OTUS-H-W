using DI;
using Events.Effects;
using Game.EventBus;
using Pipeline;

namespace Handlers.Turn
{
    public class ChangeTargetEffectHandler : BaseHandler<ChangeTargetEffect>
    {
        private EffectStack effectStack;

        [Inject]
        private void Construct(EventBus eventBus, EffectStack effectStack)
        {
            base.Construct(eventBus);
            this.effectStack = effectStack;
        }

        protected override void HandleEvent(ChangeTargetEffect evt)
        {
            evt.Target.entity = evt.NewTarget;

            if (!effectStack.IsEmpty())
            {
                EventBus.RaiseEvent(effectStack.Pop());
            }
        }
    }
}