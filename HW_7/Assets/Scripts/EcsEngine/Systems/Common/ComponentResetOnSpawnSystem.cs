using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public class ComponentResetOnSpawnSystem<T> : IEcsRunSystem where T : struct
    {
        private readonly EcsCustomInject<EntityPoolRegistry> poolRegistry;
        private readonly EcsFilterInject<Inc<SpawnEvent, T>> filter;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter.Value)
            {
                if (poolRegistry.Value.HasEntity(entity))
                {
                    poolRegistry.Value.ResetComponent<T>(entity);
                }
            }
        }
    }
}