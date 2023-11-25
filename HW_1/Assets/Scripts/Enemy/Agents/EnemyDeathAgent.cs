using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(DeathComponent))]
    public class EnemyDeathAgent : MonoBehaviour,
        IGameStartListener,
        IGameFinishListener
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

        public void OnStart()
        {
            deathComponent.Enable();
            this.deathComponent.DeathEvent += this.OnEnemyDeath;
        }

        public void OnFinish()
        {
            deathComponent.Disable();
            this.deathComponent.DeathEvent -= this.OnEnemyDeath;
        }

        private void OnEnemyDeath()
        {
            this.enemyManager.DespawnEnemy(enemy);
        }
    }
}