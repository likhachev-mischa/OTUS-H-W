using System;
using JetBrains.Annotations;

namespace MVVM
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public class GameInstallerAttribute : Attribute
    {
    }
}