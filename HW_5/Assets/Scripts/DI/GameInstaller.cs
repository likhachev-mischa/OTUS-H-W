using System;
using System.Collections.Generic;
using System.Reflection;

namespace MVVM
{
    public abstract class GameInstaller :
        IGameListenerProvider,
        IServiceProvider,
        IInjectProvider
    {
        public IEnumerable<IGameListener> ProvideListeners()
        {
            FieldInfo[] fields = GetType().GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            foreach (FieldInfo field in fields)
            {
                if (field.IsDefined(typeof(ListenerAttribute)) &&
                    field.GetValue(this) is IGameListener gameListener)
                {
                    yield return gameListener;
                }
            }
        }

        public IEnumerable<(Type, object)> ProvideServices()
        {
            FieldInfo[] fields = GetType().GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            foreach (FieldInfo field in fields)
            {
                var attribute = field.GetCustomAttribute<ServiceAttribute>();
                if (attribute != null)
                {
                    Type type = attribute.Contract;
                    object service = field.GetValue(this);
                    yield return (type, service);
                }
            }
        }

        public void Inject(ServiceLocator serviceLocator)
        {
            FieldInfo[] fields = GetType().GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            foreach (FieldInfo field in fields)
            {
                object target = field.GetValue(this);
                DependencyInjector.Inject(target, serviceLocator);
            }
        }
    }
}