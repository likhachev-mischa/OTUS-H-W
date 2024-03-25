using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable, InlineProperty]
    public sealed class AtomicValue<T> : IAtomicValue<T>
    {
        public T Value
        {
            get { return this.value; }
        }

        [SerializeField, HideLabel]
        private T value;

        public AtomicValue(T value)
        {
            this.value = value;
        }
    }
}