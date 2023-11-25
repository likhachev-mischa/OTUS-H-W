using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : MonoBehaviour
    {
        private EnemyAttackAgent attackAgent;
        private EnemyMoveAgent moveAgent;
        private EnemyDeathAgent deathAgent;

        private HealthComponent healthComponent;
        private int initialHealth;
        private GameManager gameManager;

        private void Awake()
        {
            this.attackAgent = this.GetComponent<EnemyAttackAgent>();
            this.moveAgent = this.GetComponent<EnemyMoveAgent>();
            this.deathAgent = this.GetComponent<EnemyDeathAgent>();

            this.healthComponent = this.GetComponent<HealthComponent>();
            this.initialHealth = healthComponent.Health;
        }

        public void SetManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Enable()
        {
            this.gameManager.AddListener(this.moveAgent);
            this.gameManager.AddListener(this.deathAgent);

            this.deathAgent.OnStart();

            this.healthComponent.Health = this.initialHealth;
            this.moveAgent.OnTargetReached += this.OnTargetReached;
        }

        public void Disable()
        {
            this.gameManager.RemoveListener(this.attackAgent);
            this.gameManager.RemoveListener(this.moveAgent);
            this.gameManager.RemoveListener(this.deathAgent);

            this.attackAgent.OnFinish();
            this.deathAgent.OnFinish();

            this.moveAgent.OnTargetReached -= this.OnTargetReached;
        }

        public void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        public void SetDestination(Vector2 destination)
        {
            this.moveAgent.SetDestination(destination);
        }

        public void SetTarget(GameObject target)
        {
            this.attackAgent.SetTarget(target);
        }

        private void OnTargetReached()
        {
            this.gameManager.RemoveListener(this.moveAgent);
            this.gameManager.AddListener(this.attackAgent);

            this.attackAgent.OnStart();
        }
    }
}