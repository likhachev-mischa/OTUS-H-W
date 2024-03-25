using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIModule
{
    [Serializable, InlineProperty]
    public abstract class BlackboardValue<T> : IBlackboardValue
    {
        public ushort Key => this.key;
        public object Value => this.value;

        [SerializeField, BlackboardKey]
        internal ushort key;

        [SerializeField]
        internal T value;
    }
}