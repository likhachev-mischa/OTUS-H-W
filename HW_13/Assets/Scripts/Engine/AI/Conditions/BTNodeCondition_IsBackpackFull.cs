using System;
using AIModule;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class BTNodeCondition_IsBackpackFull : IAICondition
    {
        [SerializeField, BlackboardKey]
        private ushort character;

        public bool Check(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(character, out IAtomicObject characterObject))
            {
                return false;
            }

            if (!characterObject.TryGet(ObjectAPI.ResourceStorage, out ResourceStorage backpack))
            {
                return false;
            }

            return backpack.IsFull();
        }
    }
}