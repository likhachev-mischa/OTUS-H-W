using System;
using Sirenix.OdinInspector;

namespace Game
{
    [Serializable, InlineProperty]
    public sealed class AtomicEvent : IAtomicEvent
    {
        private event Action _event;

        public void Subscribe(Action action)
        {
            _event += action;
        }

        public void Unsubscribe(Action action)
        {
            _event -= action;
        }

        [Button]
        public void Invoke()
        {
            _event?.Invoke();
        }
    }

    [Serializable, InlineProperty]
    public sealed class AtomicEvent<T> : IAtomicEvent<T>
    {
        private event Action<T> _event;

        public void Subscribe(Action<T> action)
        {
            _event += action;
        }

        public void Unsubscribe(Action<T> action)
        {
            _event -= action;
        }

        [Button]
        public void Invoke(T value)
        {
            _event?.Invoke(value);
        }
    }
}