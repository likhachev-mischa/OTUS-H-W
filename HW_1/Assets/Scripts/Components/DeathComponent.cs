using System;
using UnityEngine;

namespace ShootEmUp
{
    namespace Components
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

            private void OnEnable()
            {
                this.healthComponent.TakeDamageEvent += this.OnDeath;
            }

            private void OnDisable()
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
}