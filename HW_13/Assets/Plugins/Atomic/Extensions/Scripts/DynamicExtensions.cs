using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Atomic.Behaviours;
using Atomic.Objects;

namespace Atomic.Extensions
{
    public static class DynamicExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddElement(this AtomicBehaviour it, string name, ILogic value)
        {
            if (it.AddProperty(name, value))
            {
                it.AddLogic(value);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveElement(this AtomicBehaviour it, string name)
        {
            if (it.RemoveProperty(name, out var value) && value is ILogic logic)
            {
                it.RemoveLogic(logic);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComponent(this AtomicBehaviour it, ILogic component, bool @override = true)
        {
            AddComponent((AtomicEntity) it, component, @override);
            it.AddLogic(component);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveComponent<T>(this AtomicBehaviour it) where T : ILogic
        {
            RemoveComponent<T>((AtomicEntity) it);
            it.RemoveLogic<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveComponent(this AtomicBehaviour it, ILogic component)
        {
            RemoveComponent((AtomicEntity) it, component);
            it.RemoveLogic(component);
        }

        public static void AddComponent(this AtomicEntity it, object component, bool @override = true)
        {
            Type componentType = component.GetType();

            IEnumerable<string> types = AtomicScanner.ScanTypes(componentType);
            IEnumerable<PropertyInfo> values = AtomicScanner.ScanValues(componentType);

            it.AddTypes(types);

            if (@override)
            {
                foreach (var valueInfo in values)
                {
                    string key = valueInfo.id;
                    object value = valueInfo.value(component);
                    it.SetProperty(key, value);
                }
            }
            else
            {
                foreach (var valueInfo in values)
                {
                    string key = valueInfo.id;
                    object value = valueInfo.value(component);
                    it.AddProperty(key, value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveComponent<T>(this AtomicEntity it)
        {
            it.RemoveComponent(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveComponent(this AtomicEntity it, object component)
        {
            it.RemoveComponent(component.GetType());
        }

        public static void RemoveComponent(this AtomicEntity it, Type componentType)
        {
            IEnumerable<string> types = AtomicScanner.ScanTypes(componentType);
            it.RemoveTypes(types);

            IEnumerable<PropertyInfo> values = AtomicScanner.ScanValues(componentType);
            foreach (var valueInfo in values)
            {
                it.RemoveProperty(valueInfo.id);
            }
        }
    }
}