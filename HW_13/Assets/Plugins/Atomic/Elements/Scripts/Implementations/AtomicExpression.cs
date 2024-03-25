using System;
using System.Collections.Generic;
using Atomic.Elements;
using Sirenix.OdinInspector;

// ReSharper disable ParameterTypeCanBeEnumerable.Global
// ReSharper disable PublicConstructorInAbstractClass

namespace Atomic.Elements
{
    [Serializable, InlineProperty]
    public abstract class AtomicExpression<T> : IAtomicExpression<T>
    {
        private readonly List<IAtomicValue<T>> members;

        public AtomicExpression()
        {
            this.members = new List<IAtomicValue<T>>();
        }

        public AtomicExpression(params IAtomicValue<T>[] members)
        {
            this.members = new List<IAtomicValue<T>>(members);
        }

        public AtomicExpression(IEnumerable<IAtomicValue<T>> members)
        {
            this.members = new List<IAtomicValue<T>>(members);
        }

        public void Append(IAtomicValue<T> member)
        {
            if (member != null)
            {
                this.members.Add(member);
            }
        }

        public void Remove(IAtomicValue<T> member)
        {
            if (member != null)
            {
                this.members.Remove(member);
            }
        }

        [Button]
        public T Invoke()
        {
            return this.Invoke(this.members);
        }

        protected abstract T Invoke(IReadOnlyList<IAtomicValue<T>> members);
    }

    [Serializable, InlineProperty]
    public abstract class AtomicExpression<T, R> : IAtomicExpression<T, R>
    {
        private readonly List<IAtomicFunction<T, R>> members = new();

        public AtomicExpression()
        {
            this.members = new List<IAtomicFunction<T, R>>();
        }

        public AtomicExpression(params IAtomicFunction<T, R>[] members)
        {
            this.members = new List<IAtomicFunction<T, R>>(members);
        }

        public AtomicExpression(IEnumerable<IAtomicFunction<T, R>> members)
        {
            this.members = new List<IAtomicFunction<T, R>>(members);
        }
        
        public void Append(IAtomicFunction<T, R> member)
        {
            this.members.Add(member);
        }

        public void Remove(IAtomicFunction<T, R> member)
        {
            this.members.Remove(member);
        }

        [Button]
        public R Invoke(T args)
        {
            return this.Invoke(this.members, args);
        }

        protected abstract R Invoke(IReadOnlyList<IAtomicFunction<T, R>> members, T args);
    }
}