using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable, InlineProperty]
    public class AtomicVariable<T> : IAtomicVariableObservable<T>, IDisposable
    {
        public T Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                this.onChanged?.Invoke(value);
            }
        }

        public void Subscribe(Action<T> listener)
        {
            this.onChanged += listener;
        }

        public void Unsubscribe(Action<T> listener)
        {
            this.onChanged -= listener;
        }

        private Action<T> onChanged;

        [SerializeField, HideLabel, OnValueChanged(nameof(OnValueChanged))]
        private T value;

        public AtomicVariable()
        {
            this.value = default;
        }

        public AtomicVariable(T value)
        {
            this.value = value;
        }

        private void OnValueChanged(T value)
        {
            this.onChanged?.Invoke(value);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onChanged);
        }
    }
}