using System;
using AIModule;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class BTNode_ExtractResource : BTNode
    {
        public override string Name => "Extract Resource";

        [SerializeField, BlackboardKey]
        private ushort character;

        [SerializeField, BlackboardKey]
        private ushort resource;

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(character, out IAtomicObject characterObject) ||
                !blackboard.TryGetObject(resource, out IAtomicObject resourceObject))
            {
                return BTState.FAILURE;
            }

            if (!characterObject.TryGet(ObjectAPI.GatherRequest, out IAtomicAction gatherRequest))
            {
                return BTState.FAILURE;
            }

            if (!characterObject.TryGet(ObjectAPI.ResourceStorage, out ResourceStorage characterStorage) ||
                !resourceObject.TryGet(ObjectAPI.ResourceStorage, out ResourceStorage resourceStorage))
            {
                return BTState.FAILURE;
            }

            if (resourceStorage.IsEmpty())
            {
                return BTState.SUCCESS;
            }

            if (characterStorage.IsFull())
            {
                return BTState.FAILURE;
            }

            gatherRequest.Invoke();
            return BTState.RUNNING;
        }
    }
}