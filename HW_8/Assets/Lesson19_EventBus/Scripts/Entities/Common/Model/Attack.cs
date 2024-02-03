using System;
using Lessons.Game;
using Lessons.Utils;

namespace Lessons.Entities.Common.Model
{
    [Serializable]
    public sealed class Attack
    {
        public AtomicVariable<Weapon> weapon;
    }
}