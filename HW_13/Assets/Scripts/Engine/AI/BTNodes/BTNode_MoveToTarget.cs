using System;
using AIModule;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class BTNode_MoveToTarget : BTNode
    {
        public override string Name => "Move To Target";

        [SerializeField, BlackboardKey]
        private ushort character;

        [SerializeField, BlackboardKey]
        private ushort target;

        [SerializeField, BlackboardKey]
        private ushort stoppingDistance;

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(character, out IAtomicObject characterObject) ||
                !blackboard.TryGetObject(target, out IAtomicObject targetObject) ||
                !blackboard.TryGetFloat(stoppingDistance, out float distance))
            {
                return BTState.FAILURE;
            }

            if (!characterObject.TryGet(ObjectAPI.Transform, out Transform characterTransform) ||
                !targetObject.TryGet(ObjectAPI.Transform, out Transform objectTransform))
            {
                return BTState.FAILURE;
            }

            Vector3 distanceVector = objectTransform.position - characterTransform.position;

            if (distanceVector.sqrMagnitude <= distance * distance)
            {
                return BTState.SUCCESS;
            }

            if (!characterObject.TryGet(ObjectAPI.MoveStepRequest, out IAtomicAction<Vector3> moveStepRequest))
            {
                return BTState.FAILURE;
            }

            moveStepRequest.Invoke(distanceVector.normalized);
            return BTState.RUNNING;
        }
    }
}