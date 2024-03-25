using System;
using AIModule;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class BTNodeCondition_IsBarnFull : IAICondition
    {
        [SerializeField, BlackboardKey]
        private ushort barn;

        public bool Check(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(barn, out IAtomicObject barnObject))
            {
                return false;
            }

            if (!barnObject.TryGet(ObjectAPI.ResourceStorage, out ResourceStorage storage))
            {
                return false;
            }

            return storage.IsFull();
        }
    }
}