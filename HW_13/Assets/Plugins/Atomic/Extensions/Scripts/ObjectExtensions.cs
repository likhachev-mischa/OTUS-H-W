using System.Runtime.CompilerServices;
using Atomic.Elements;
using Atomic.Objects;

namespace Atomic.Extensions
{
    public static class ObjectExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicValue<T> GetValue<T>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicValue<T>>(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IAtomicObject it, string name, out IAtomicValue<T> result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicVariable<T> GetVariable<T>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicVariable<T>>(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetVariable<T>(this IAtomicObject it, string name, out IAtomicVariable<T> result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T> GetFunction<T>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicFunction<T>>(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFunction<T>(this IAtomicObject it, string name, out IAtomicFunction<T> result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T1, T2> GetFunction<T1, T2>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicFunction<T1, T2>>(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFunction<T1, T2>(
            this IAtomicObject it,
            string name,
            out IAtomicFunction<T1, T2> result
        )
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T1, T2, T3> GetFunction<T1, T2, T3>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicFunction<T1, T2, T3>>(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFunction<T1, T2, T3>(
            this IAtomicObject it,
            string name,
            out IAtomicFunction<T1, T2, T3> result
        )
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction GetAction(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicAction>(name);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAction(this IAtomicObject it, string name, out IAtomicAction result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction<T> GetAction<T>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicAction<T>>(name);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAction<T>(this IAtomicObject it, string name, out IAtomicAction<T> result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction<T1, T2> GetAction<T1, T2>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicAction<T1, T2>>(name);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAction<T1, T2>(this IAtomicObject it, string name, out IAtomicAction<T1, T2> result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeAction(this IAtomicObject it, string name)
        {
            it.GetAction(name)?.Invoke();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeAction<T>(this IAtomicObject it, string name, T args)
        {
            it.GetAction<T>(name)?.Invoke(args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T InvokeFunction<T>(this IAtomicObject it, string name)
        {
            IAtomicFunction<T> function = it.GetFunction<T>(name);
            if (function != null)
            {
                return function.Invoke();
            }

            return default;
        }

        public static IAtomicSetter<T> GetSetter<T>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicSetter<T>>(name);
        }

        public static void SetValue<T>(this IAtomicObject it, string name, T value)
        {
            if (it.TryGet(name, out IAtomicSetter<T> setter))
            {
                setter.Value = value;
            }
        }

        public static IAtomicExpression<T> GetExpression<T>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicExpression<T>>(name);
        }
        
        public static bool TryGetExpression<T>(this IAtomicObject it, string name, out IAtomicExpression<T> result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicObservable GetObservable(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicObservable>(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetObservable(this IAtomicObject it, string name, out IAtomicObservable result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicObservable<T> GetObservable<T>(this IAtomicObject it, string name)
        {
            return it.Get<IAtomicObservable<T>>(name);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetObservable<T>(this IAtomicObject it, string name, out IAtomicObservable<T> result)
        {
            return it.TryGet(name, out result) && result != null;
        }

    }
}