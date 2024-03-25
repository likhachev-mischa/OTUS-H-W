using System;
using AIModule;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class BTNode_FindResource : BTNode
    {
        public override string Name => "Find Resource";

        [SerializeField, BlackboardKey]
        private ushort character;

        [SerializeField, BlackboardKey]
        private ushort resourceService;

        [SerializeField, BlackboardKey]
        private ushort targetResource;

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(resourceService, out GameObject resourceServiceObject) ||
                !blackboard.TryGetObject(character, out IAtomicObject characterObject))
            {
                return BTState.FAILURE;
            }

            if (!characterObject.TryGet(ObjectAPI.Transform, out Transform characterTransform) ||
                !resourceServiceObject.TryGetComponent(out ResourceService resourceServiceComponent))
            {
                return BTState.FAILURE;
            }

            if (!resourceServiceComponent.FindClosestResource(characterTransform.position,
                    out IAtomicObject resourceObject))
            {
                return BTState.FAILURE;
            }

            blackboard.SetObject(targetResource, resourceObject);
            return BTState.SUCCESS;
        }
    }
}