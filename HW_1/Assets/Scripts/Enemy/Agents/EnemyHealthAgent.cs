using System;
using ShootEmUp.Components;
using UnityEngine;

namespace ShootEmUp
{
    namespace Enemy
    {
         public class EnemyHealthAgent : EnemyAgent, IKillable
        {
            public event Action<int> TakeDamageEvent;

            public event Action<EnemyProvider> DeathEvent;

            [SerializeField] private int health;


            public int Health
            {
                get => health;
                set
                {
                    health = value;
                    TakeDamageEvent?.Invoke(value);
                }

            }

            private DeathComponent deathComponent;

            private void Awake()
            {
                deathComponent = new DeathComponent(this);
            }

            private void OnEnable()
            {
                deathComponent.Enable();
            }

            private void OnDisable()
            {
                deathComponent.Disable();
            }

            public void Death()
            {
                DeathEvent?.Invoke(this.GetComponentInChildren<EnemyProvider>());
            }
        }
    }
}