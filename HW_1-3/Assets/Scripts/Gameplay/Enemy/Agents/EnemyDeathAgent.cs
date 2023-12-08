using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(DeathComponent))]
    public class EnemyDeathAgent : MonoBehaviour
    {
        public event Action OnDeath;

        private DeathComponent deathComponent;

        private void Awake()
        {
            deathComponent = GetComponent<DeathComponent>();
        }

        public void Enable()
        {
            deathComponent.Enable();
            deathComponent.DeathEvent += OnEnemyDeath;
            enabled = true;
        }

        public void Disable()
        {
            deathComponent.Disable();
            deathComponent.DeathEvent -= OnEnemyDeath;
            enabled = false;
        }

        private void OnEnemyDeath()
        {
            OnDeath?.Invoke();
        }
    }
}