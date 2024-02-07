using UnityEngine;

namespace Pipeline.Tasks
{
    public class FinishGameTask : Task
    {
        private Pipeline pipeline;

        public FinishGameTask(Pipeline pipeline)
        {
            this.pipeline = pipeline;
        }

        protected override void OnRun()
        {
            pipeline.OnFinished += OnPipelineFinished;
        }

        protected override void OnFinish()
        {
            pipeline.OnFinished -= OnPipelineFinished;
        }

        private void OnPipelineFinished()
        {
            pipeline.Clear();
            Finish();
        }
    }
}