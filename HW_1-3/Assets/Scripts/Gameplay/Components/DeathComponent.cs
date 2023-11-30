using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(HealthComponent))]
    public sealed class DeathComponent : MonoBehaviour
    {
        private HealthComponent healthComponent;
        public event Action DeathEvent;

        private void Awake()
        {
            this.healthComponent = this.GetComponent<HealthComponent>();
        }

        public void Enable()
        {
            this.healthComponent.TakeDamageEvent += this.OnDeath;
        }

        public void Disable()
        {
            this.healthComponent.TakeDamageEvent += this.OnDeath;
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