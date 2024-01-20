using System;

namespace Game
{
    public interface IAtomicVariable<T> : IAtomicValue<T>
    {
        event Action<T> ValueChanged;
        new T Value { get; set; }
    }
}