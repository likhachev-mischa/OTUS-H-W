using System;
using Lessons.Utils;

namespace Lessons.Entities.Common.Model
{
    [Serializable]
    public sealed class Stats
    {
        public AtomicVariable<int> strength = 1;
    }
}