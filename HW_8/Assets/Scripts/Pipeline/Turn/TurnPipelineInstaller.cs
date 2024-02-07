using System;
using DI;
using Pipeline;
using Pipeline.Tasks;

namespace Installers
{
    public sealed class TurnPipelineInstaller : IInitializable, IDisposable 
    {
        private TurnPipeline turnPipeline;
        private IObjectResolver objectResolver;

        [Inject]
        public void Construct(TurnPipeline turnPipeline, IObjectResolver objectResolver)
        {
            this.turnPipeline = turnPipeline;
            this.objectResolver = objectResolver;
        }

        void IInitializable.Initialize()
        {
            
            turnPipeline.AddTask(new StartTurnTask());
            
            //turnPipeline.AddTask(objectResolver.CreateInstance<HandleVisualPipelineTask>());
            turnPipeline.AddTask(objectResolver.CreateInstance<HeroSelectionTask>());
            turnPipeline.AddTask(objectResolver.CreateInstance<HandleVisualPipelineTask>());
            //turnPipeline.AddTask(objectResolver.CreateInstance<HandleVisualPipelineTask>());
            turnPipeline.AddTask(objectResolver.CreateInstance<EnemySelectionTask>());
            turnPipeline.AddTask(objectResolver.CreateInstance<HandleVisualPipelineTask>());
           // turnPipeline.AddTask(objectResolver.CreateInstance<HandleVisualPipelineTask>());
            //turnPipeline.AddTask(objectResolver.CreateInstance<FinishTurnTask>());
            //turnPipeline.AddTask(objectResolver.CreateInstance<HandleVisualPipelineTask>());
        }

        void IDisposable.Dispose()
        {
            turnPipeline.Clear();
            objectResolver?.Dispose();
        }
    }
}