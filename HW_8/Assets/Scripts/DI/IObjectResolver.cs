using System;

namespace DI
{
    public interface IObjectResolver : IDisposable
    {
        T CreateInstance<T>() where T : new();
    }
}