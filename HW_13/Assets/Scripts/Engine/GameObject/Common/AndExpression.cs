using System;
using System.Collections.Generic;
using Atomic.Elements;

namespace Game.Engine
{
    [Serializable]
    public sealed class AndExpression : AtomicExpression<bool>
    {
        public AndExpression()
        {
        }

        public AndExpression(IEnumerable<IAtomicValue<bool>> members) : base(members)
        {
        }

        protected override bool Invoke(IReadOnlyList<IAtomicValue<bool>> members)
        {
            foreach (var member in members)
            {
                if (!member.Value)
                {
                    return false;
                }
            }

            return true;
        }
    }
    
    [Serializable]
    public sealed class AndExpression<T> : AtomicExpression<T, bool>
    {
        public AndExpression()
        {
        }

        public AndExpression(IEnumerable<IAtomicFunction<T, bool>> members) : base(members)
        {
        }

        protected override bool Invoke(IReadOnlyList<IAtomicFunction<T, bool>> members, T args)
        {
            foreach (var member in members)
            {
                if (!member.Invoke(args))
                {
                    return false;
                }
            }

            return true;
        }
    }
}