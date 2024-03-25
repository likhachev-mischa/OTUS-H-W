using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine
{
    ///Содержит информацию о всех ресурсах на карте
    public sealed class ResourceService : MonoBehaviour
    {
        private AtomicEntity[] resources;
        
        private void Awake()
        {
            this.resources = this.GetComponentsInChildren<AtomicEntity>();
        }

        public bool FindClosestResource(Vector3 position, out IAtomicObject result)
        {
            float minDistance = float.MaxValue;
            result = default;
            
            for (int i = 0, count = this.resources.Length; i < count; i++)
            {
                IAtomicObject resource = this.resources[i];
                if (!resource.GetValue<bool>(ObjectAPI.IsActive).Value)
                {
                    continue;
                }

                Vector3 resourcePosition = resource.Get<Transform>(ObjectAPI.Transform).position;
                Vector3 distanceVector = resourcePosition - position;
                distanceVector.y = 0;

                float resourceDistance = distanceVector.sqrMagnitude;
                if (resourceDistance < minDistance)
                {
                    minDistance = resourceDistance;
                    result = resource;
                }
            }

            return result != default;
        }
    }
}