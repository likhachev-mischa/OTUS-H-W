using System;
using System.Linq;
using System.Reflection;
using VContainer;

namespace Lessons.Common
{
    public static class ObjectResolverExtensions
    {
        public static T CreateInstance<T>(this IObjectResolver objectResolver)
        {
            Type type = typeof(T);
            ConstructorInfo constructor = type.GetConstructors().FirstOrDefault();

            if (constructor is null)
            {
                throw new VContainerException(type, "Failed to find suitable constructor!");
            }

            ParameterInfo[] parametersInfo = constructor.GetParameters();
            object[] parameters = new object[parametersInfo.Length];

            for (int i = 0; i < parametersInfo.Length; i++)
            {
                parameters[i] = objectResolver.Resolve(parametersInfo[i].ParameterType);
            }
            
            T instance = (T)constructor.Invoke(parameters);
            objectResolver.Inject(instance);

            return instance;
        }
    }
}