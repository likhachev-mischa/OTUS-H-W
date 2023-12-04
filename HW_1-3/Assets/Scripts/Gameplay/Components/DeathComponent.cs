using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class DeathComponent : MonoBehaviour
    {
        private HealthComponent healthComponent;
        public event Action DeathEvent;

        private void Awake()
        {
            healthComponent = GetComponent<HealthComponent>();
        }

        public void Enable()
        {
            healthComponent.TakeDamageEvent += OnDeath;
        }

        public void Disable()
        {
            healthComponent.TakeDamageEvent += OnDeath;
        }

        private void OnDeath()
        {
            if (healthComponent.Health <= 0)
            {
                DeathEvent?.Invoke();
            }
        }
    }
}