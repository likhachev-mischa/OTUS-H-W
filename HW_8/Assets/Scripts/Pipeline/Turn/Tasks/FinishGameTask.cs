using DI;
using UnityEngine;

namespace Pipeline.Tasks
{
    public class FinishGameTask : Task
    {
        private TurnPipeline pipeline;
        private GameManager gameManager;

        [Inject]
        public void Construct(TurnPipeline pipeline, GameManager gameManager)
        {
            this.pipeline = pipeline;
            this.gameManager = gameManager;
        }

        protected override void OnRun()
        {
            Debug.LogWarning("GAME FINISHED");
            gameManager.FinishGame();
            //pipeline.OnFinished += OnPipelineFinished;
        }

        /*protected override void OnFinish()
        {
            pipeline.OnFinished -= OnPipelineFinished;
        }

        private void OnPipelineFinished()
        {
            Debug.LogWarning("GAME FINISHED");
            gameManager.FinishGame();
            Finish();
        }*/
    }
}