using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class BulletDestroySystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<BulletTag, Inactive>> filter;
        private EcsCustomInject<EntityManager> entityManager;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entity in this.filter.Value)
            {
                Debug.Log("DESTROYED");
                this.entityManager.Value.Destroy(entity);
            }
        }
    }
}