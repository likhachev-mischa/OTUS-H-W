using System.Collections.Generic;
using Events.Effects;

namespace Pipeline
{
    public sealed class EffectStack
    {
        private Stack<IEffect> stack = new();

        public void Push(IEffect effect)
        {
            stack.Push(effect);
        }

        public IEffect Pop()
        {
            return stack.Pop();
        }
        
        public bool TryPop(out IEffect effect)
        {
           return stack.TryPop(out effect);
        }

        public bool IsEmpty()
        {
            return stack.Count == 0;
        }

        public void Clear()
        {
            stack.Clear();
        }
    }
}