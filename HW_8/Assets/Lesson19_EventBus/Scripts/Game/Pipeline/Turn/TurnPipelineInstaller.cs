using System;
using JetBrains.Annotations;
using Lessons.Common;
using Lessons.Game.Pipeline.Turn.Tasks;
using VContainer;
using VContainer.Unity;

namespace Lessons.Game.Pipeline.Turn
{
    [UsedImplicitly]
    public sealed class TurnPipelineInstaller : IInitializable, IDisposable
    {
        private readonly TurnPipeline _turnPipeline;
        private readonly IObjectResolver _objectResolver;

        public TurnPipelineInstaller(TurnPipeline turnPipeline, IObjectResolver objectResolver)
        {
            _turnPipeline = turnPipeline;
            _objectResolver = objectResolver;
        }

        void IInitializable.Initialize()
        {
            _turnPipeline.AddTask(new StartTurnTask());
            _turnPipeline.AddTask(_objectResolver.CreateInstance<PlayerTurnTask>());
            _turnPipeline.AddTask(_objectResolver.CreateInstance<HandleVisualPipelineTask>());
            _turnPipeline.AddTask(new FinishTurnTask());
        }

        void IDisposable.Dispose()
        {
            _turnPipeline.Clear();
            _objectResolver?.Dispose();
        }
    }
}