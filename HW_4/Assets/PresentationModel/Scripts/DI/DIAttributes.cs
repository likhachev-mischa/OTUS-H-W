using System;
using JetBrains.Annotations;

namespace MVVM
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
            this.Contract = contract;
        }
    }

    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ListenerAttribute : Attribute
    {
    }
}