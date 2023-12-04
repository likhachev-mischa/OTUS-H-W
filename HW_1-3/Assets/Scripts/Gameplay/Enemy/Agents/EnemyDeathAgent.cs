using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(DeathComponent))]
    public class EnemyDeathAgent : MonoBehaviour
    {
        public event Action<Enemy> OnDeath; 

        private DeathComponent deathComponent;
        private Enemy enemy;

        private void Awake()
        {
            deathComponent = GetComponent<DeathComponent>();
            enemy = GetComponent<Enemy>();
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
            OnDeath?.Invoke(enemy);
        }
    }
}