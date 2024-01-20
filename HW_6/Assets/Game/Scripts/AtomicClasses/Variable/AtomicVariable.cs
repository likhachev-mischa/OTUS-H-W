using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    [Serializable, InlineProperty]
    public class AtomicVariable<T> : IAtomicVariable<T>
    {
        public event Action<T> ValueChanged;

        [HideLabel, OnValueChanged("OnValueChangedInEditor")] [SerializeField]
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke(value);
            }
        }

#if UNITY_EDITOR
        private void OnValueChangedInEditor(T _)
        {
            ValueChanged?.Invoke(_value);
        }
#endif
    }
}