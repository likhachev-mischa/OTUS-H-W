using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(DeathComponent))]
    public class EnemyDeathAgent : MonoBehaviour
    {
        private EnemyManager enemyManager;
        private DeathComponent deathComponent;

        private Enemy enemy;
        
        public void Construct(EnemyManager enemyManager)
        {
            this.enemyManager = enemyManager;
        }

        private void Awake()
        {
            this.deathComponent = this.GetComponent<DeathComponent>();
            this.enemy = this.GetComponent<Enemy>();
        }

        public void Enable()
        {
            deathComponent.Enable();
            this.deathComponent.DeathEvent += this.OnEnemyDeath;
            this.enabled = true;
        }

        public void Disable()
        {
            deathComponent.Disable();
            this.deathComponent.DeathEvent -= this.OnEnemyDeath;
            this.enabled = false;
        }

        private void OnEnemyDeath()
        {
            this.enemyManager.DespawnEnemy(enemy);
        }
    }
}