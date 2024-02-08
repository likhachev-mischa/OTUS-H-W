using DI;
using UnityEngine;

namespace Pipeline.Tasks
{
    public sealed class HandleVisualPipelineTask : Task
    {
        private VisualPipeline visualPipeline;

        [Inject]
        public void Construct(VisualPipeline visualPipeline)
        {
            this.visualPipeline = visualPipeline;
        }

        protected override void OnRun()
        {
            visualPipeline.OnFinished += OnVisualPipelineFinished;

            visualPipeline.Run();
        }

        protected override void OnFinish()
        {
            visualPipeline.OnFinished -= OnVisualPipelineFinished;
        }

        private void OnVisualPipelineFinished()
        {
            visualPipeline.Clear();
            Finish();
        }
    }
}