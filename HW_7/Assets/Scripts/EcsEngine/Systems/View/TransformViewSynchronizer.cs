using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class TransformViewSynchronizer : IEcsPostRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView, Position>> filter;
        private readonly EcsPoolInject<Rotation> rotationPool;

        void IEcsPostRunSystem.PostRun(IEcsSystems systems)
        {
            EcsPool<Rotation> rotationPool = this.rotationPool.Value;

            foreach (int entity in this.filter.Value)
            {
                ref TransformView transform = ref this.filter.Pools.Inc1.Get(entity);
                Position position = this.filter.Pools.Inc2.Get(entity);
                
                transform.value.position = position.value;

                if (rotationPool.Has(entity))
                {
                    Quaternion rotation = rotationPool.Get(entity).value;
                    transform.value.rotation = rotation;
                }
            }
        }
    }
}