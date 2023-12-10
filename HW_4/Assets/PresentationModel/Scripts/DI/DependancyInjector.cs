using System;
using System.Reflection;

namespace MVVM
{
    public static class DependencyInjector
    {
        public static void Inject(object target, ServiceLocator locator)
        {
            Type type = target.GetType();
            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.FlattenHierarchy
            );

            for (var index = 0; index < methods.Length; index++)
            {
                MethodInfo method = methods[index];
                if (method.IsDefined(typeof(InjectAttribute)))
                {
                    InvokeInjection(method, target, locator);
                }
            }
        }

        private static void InvokeInjection(MethodInfo method, object target, ServiceLocator locator)
        {
            ParameterInfo[] parameters = method.GetParameters();

            int count = parameters.Length;
            var args = new object[count];

            for (var i = 0; i < count; ++i)
            {
                ParameterInfo parameter = parameters[i];
                Type type = parameter.ParameterType;

                object service = locator.GetService(type);
                args[i] = service;
            }

            method.Invoke(target, args);
        }
    }
}