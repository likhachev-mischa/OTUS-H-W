using System;
using AIModule;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class BTNode_UnloadResources : BTNode
    {
        public override string Name => "Unload Resources";

        [SerializeField, BlackboardKey]
        private ushort character;

        [SerializeField, BlackboardKey]
        private ushort targetStorage;

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(character, out IAtomicObject characterObject) ||
                !blackboard.TryGetObject(targetStorage, out IAtomicObject storageObject))
            {
                return BTState.FAILURE;
            }

            if (!characterObject.TryGet(ObjectAPI.ResourceStorage, out ResourceStorage characterStorage) ||
                !storageObject.TryGet(ObjectAPI.ResourceStorage, out ResourceStorage barnStorage))
            {
                return BTState.FAILURE;
            }

            if (barnStorage.IsFull())
            {
                return BTState.FAILURE;
            }

            int resourcesToExtract = barnStorage.FreeSlots;
            resourcesToExtract = characterStorage.Current > resourcesToExtract
                ? resourcesToExtract
                : characterStorage.Current;

            characterStorage.ExtractResources(resourcesToExtract);
            barnStorage.PutResources(resourcesToExtract);
            return BTState.SUCCESS;
        }
    }
}