using System;
using JetBrains.Annotations;

namespace DI
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class InjectAttribute : Attribute
    {
    }

    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ServiceAttribute : Attribute
    {
        public readonly Type Contract;

        public ServiceAttribute(Type contract)
        {
            Contract = contract;
        }
    }

    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ServiceCollectionAttribute : Attribute
    {
        public readonly Type Contract;

        public ServiceCollectionAttribute(Type contract)
        {
            Contract = contract;
        }
    }

    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ListenerAttribute : Attribute
    {
    }
}