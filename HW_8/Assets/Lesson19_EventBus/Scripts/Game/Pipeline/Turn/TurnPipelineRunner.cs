using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Lessons.Game.Pipeline.Turn
{
    public sealed class TurnPipelineRunner : MonoBehaviour
    {
        [SerializeField] private bool runOnStart = true;
        [SerializeField] private bool runOnFinish = true;
        
        private TurnPipeline _turnPipeline;

        [Inject]
        private void Construct(TurnPipeline turnPipeline)
        {
            _turnPipeline = turnPipeline;
        }

        private void OnEnable()
        {
            _turnPipeline.OnFinished += OnTurnPipelineFinished;
        }

        private void OnDisable()
        {
            _turnPipeline.OnFinished -= OnTurnPipelineFinished;
        }

        private void Start()
        {
            if (runOnStart)
                Run();
        }

        [Button]
        public void Run()
        {
            _turnPipeline.Run();
        }
        
        private void OnTurnPipelineFinished()
        {
            if (runOnFinish)
                Run();
        }
    }
}