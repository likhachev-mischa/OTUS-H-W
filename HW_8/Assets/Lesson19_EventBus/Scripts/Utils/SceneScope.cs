using Lessons.Entities;
using Lessons.Game;
using Lessons.Game.Handlers.Effects;
using Lessons.Game.Handlers.Turn;
using Lessons.Game.Handlers.Visual;
using Lessons.Game.Pipeline.Turn;
using Lessons.Game.Pipeline.Visual;
using Lessons.Game.Services;
using Lessons.Level;
using VContainer;
using VContainer.Unity;

namespace Lessons.Utils
{
    public sealed class SceneScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureLevel(builder);
            ConfigurePlayer(builder);
            ConfigureTurn(builder);
            ConfigureVisual(builder);

            builder.RegisterComponentInHierarchy<EntityInstaller>();
        }

        private void ConfigureLevel(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<TileMap>();
            builder.Register<EntityMap>(Lifetime.Singleton);
            builder.Register<LevelMap>(Lifetime.Singleton);
        }

        private void ConfigurePlayer(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<KeyboardInput>();
            builder.RegisterComponentInHierarchy<PlayerService>();
        }

        private void ConfigureTurn(IContainerBuilder builder)
        {
            builder.Register<EventBus>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<ApplyDirectionHandler>();
            builder.RegisterEntryPoint<AttackHandler>();
            builder.RegisterEntryPoint<CollideHandler>();
            builder.RegisterEntryPoint<DealDamageHandler>();
            builder.RegisterEntryPoint<DestroyHandler>();
            builder.RegisterEntryPoint<MoveHandler>();
            builder.RegisterEntryPoint<ForceDirectionHandler>();
            
            builder.RegisterEntryPoint<DealDamageEffectHandler>();
            builder.RegisterEntryPoint<PushEffectHandler>();

            builder.Register<TurnPipeline>(Lifetime.Singleton);
            builder.RegisterEntryPoint<TurnPipelineInstaller>();
            builder.RegisterComponentInHierarchy<TurnPipelineRunner>();
        }
        
        private void ConfigureVisual(IContainerBuilder builder)
        {
            builder.Register<VisualPipeline>(Lifetime.Singleton);

            builder.RegisterEntryPoint<MoveVisualHandler>();
            builder.RegisterEntryPoint<DestroyVisualHandler>();
            builder.RegisterEntryPoint<AttackVisualHandler>();
            builder.RegisterEntryPoint<DealDamageVisualHandler>();
            builder.RegisterEntryPoint<CollideVisualHandler>();
        }
    }
}