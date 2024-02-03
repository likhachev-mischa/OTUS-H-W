using Lessons.Game.Pipeline.Visual;

namespace Lessons.Game.Handlers
{
    public abstract class BaseVisualHandler<T> : BaseHandler<T>
    {
        protected readonly VisualPipeline VisualPipeline;

        protected BaseVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
        {
            VisualPipeline = visualPipeline;
        }

        protected override void HandleEvent(T evt)
        {
            
        }
    }
}