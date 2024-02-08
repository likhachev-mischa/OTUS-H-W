using DI;
using Entities.Components;
using Events.Effects;
using Game.EventBus;
using Pipeline;
using Pipeline.SFXTasks;

namespace Handlers.SFX
{
    public class TakeDamageSFXHandler : BaseHandler<ReceiveDamageEffect>
    {
        private VisualPipeline visualPipeline;

        [Inject]
        private void Construct(EventBus eventBus, VisualPipeline visualPipeline)
        {
            base.Construct(eventBus);
            this.visualPipeline = visualPipeline;
        }

        protected override void HandleEvent(ReceiveDamageEffect evt)
        {
            var health = evt.Source.Get<Health>();
            float delta = (float)health.Value / health.InitialValue;
            float lowHealth = 0.2f;

            if (delta <= lowHealth)
            {
                visualPipeline.AddTask(new TakeDamageSFXTask(evt.Source));
            }
        }
    }
}