using Content;
using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    public class UnitColorViewSynchronizer : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ColorView, Team, SpawnEvent>> filter;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in filter.Value)
            {
                ref ColorView colorView = ref filter.Pools.Inc1.Get(entity);
                Teams teams = filter.Pools.Inc2.Get(entity).value;
                colorView.value.Team = teams;
            }
        }
    }
}