using System;
using Sirenix.OdinInspector;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class AtomicProperty<T> : IAtomicVariable<T>
    {
        [ShowInInspector, ReadOnly]
        public T Value
        {
            get { return this.getter != null ? this.getter.Invoke() : default; }
            set { this.setter?.Invoke(value); }
        }

        private Func<T> getter;
        private Action<T> setter;

        public AtomicProperty()
        {
        }

        public AtomicProperty(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        public void Compose(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }
    }
}