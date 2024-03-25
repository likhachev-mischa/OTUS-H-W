using System;
using AIModule;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class BTNodeCondition_AreNoResourcesLeft : IAICondition
    {
        [SerializeField, BlackboardKey]
        private ushort resourceService;

        public bool Check(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(resourceService, out GameObject resourceObject))
            {
                return false;
            }

            if (!resourceObject.TryGetComponent(out ResourceService service))
            {
                return false;
            }

            return !service.FindClosestResource(Vector3.zero, out _);
        }
    }
}