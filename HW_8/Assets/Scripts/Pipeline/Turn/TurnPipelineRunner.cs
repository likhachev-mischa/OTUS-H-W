using System;
using DI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pipeline
{
    [Serializable]
    public sealed class TurnPipelineRunner : IInitializable, IGameStartListener, IDisposable
    {
        [SerializeField] private bool runOnStart = true;
        [SerializeField] private bool runOnFinish = true;

        private TurnPipeline _turnPipeline;

        [Inject]
        private void Construct(TurnPipeline turnPipeline)
        {
            _turnPipeline = turnPipeline;
        }

        void IInitializable.Initialize()
        {
            _turnPipeline.OnFinished += OnTurnPipelineFinished;
        }

        void IDisposable.Dispose()
        {
            _turnPipeline.OnFinished -= OnTurnPipelineFinished;
        }

        void IGameStartListener.OnStart()
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