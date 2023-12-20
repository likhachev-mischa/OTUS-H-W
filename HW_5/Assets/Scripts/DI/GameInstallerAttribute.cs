using System;
using JetBrains.Annotations;

namespace DI
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public class GameInstallerAttribute : Attribute
    {
    }
}