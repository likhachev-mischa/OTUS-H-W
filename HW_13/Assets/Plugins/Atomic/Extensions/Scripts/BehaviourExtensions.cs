using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Atomic.Behaviours;

// ReSharper disable ForCanBeConvertedToForeach

namespace Atomic.Extensions
{
    public static class BehaviourExtensions
    {
        private static readonly List<ILogic> cache = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddLogicRange(this IAtomicBehaviour it, IEnumerable<ILogic> targets)
        {
            foreach (var target in targets)
            {
                it.AddLogic(target);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddLogicRange(this IAtomicBehaviour it, params ILogic[] logics)
        {
            for (int i = 0, count = logics.Length; i < count; i++)
            {
                it.AddLogic(logics[i]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAllLogic<T>(this AtomicBehaviour it) where T : ILogic
        {
            cache.Clear();
            cache.AddRange(it.LogicElementsUnsafe());

            for (int i = 0; i < cache.Count; i++)
            {
                if (cache[i] is T logic)
                {
                    it.RemoveLogic(logic);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RemoveLogic<T>(this AtomicBehaviour it) where T : ILogic
        {
            IList<ILogic> elements = it.LogicElementsUnsafe();

            for (int i = 0, count = elements.Count; i < count; i++)
            {
                if (elements[i] is T element)
                {
                    it.RemoveLogic(element);
                    return true;
                }
            }

            return false;
        }

        public static bool FindLogic<T>(this AtomicBehaviour it, out T result) where T : ILogic
        {
            IList<ILogic> elements = it.LogicElementsUnsafe();

            for (int i = 0, count = elements.Count; i < count; i++)
            {
                if (elements[i] is T tElement)
                {
                    result = tElement;
                    return true;
                }
            }

            result = default;
            return false;
        }
    }
}