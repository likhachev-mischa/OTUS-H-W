using DI;
using Events;
using Game.EventBus;
using Pipeline;

namespace Handlers.Turn
{
    public class TurnSkipHandler : BaseHandler<TurnSkippedEvent>
    {
        private TurnPipeline turnPipeline;
        private VisualPipeline visualPipeline;

        [Inject]
        private void Construct(EventBus eventBus, TurnPipeline turnPipeline, VisualPipeline visualPipeline)
        {
            base.Construct(eventBus);
            this.turnPipeline = turnPipeline;
            this.visualPipeline = visualPipeline;
        }

        protected override void HandleEvent(TurnSkippedEvent evt)
        {
            //visualPipeline.SkipTurn();
            //visualPipeline.Clear();
            turnPipeline.SkipTurn();
            //visualPipeline.Run();
        }
    }
}