using System;
using Lessons.Utils;

namespace Lessons.Entities.Common.Components
{
    public sealed class DeathComponent
    {
        public event Action<bool> OnIsDeadChanged
        {
            add => _isDead.ValueChanged += value;
            remove => _isDead.ValueChanged -= value;
        }
        
        public bool IsDead => _isDead;

        private readonly AtomicVariable<bool> _isDead;

        public DeathComponent(AtomicVariable<bool> isDead)
        {
            _isDead = isDead;
        }

        public void Die()
        {
            _isDead.Value = true;
        }
    }
}