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

        private void Awake()
        {
            this.deathComponent = this.GetComponent<DeathComponent>();
            this.enemy = this.GetComponent<Enemy>();
            this.enemyManager = FindObjectOfType<EnemyManager>();
        }

        private void OnEnable()
        {
            this.deathComponent.DeathEvent += this.OnEnemyDeath;
        }

        private void OnDisable()
        {
            this.deathComponent.DeathEvent -= this.OnEnemyDeath;
        }

        private void OnEnemyDeath()
        {
            this.enemyManager.DespawnEnemy(enemy);
        }
    }
}