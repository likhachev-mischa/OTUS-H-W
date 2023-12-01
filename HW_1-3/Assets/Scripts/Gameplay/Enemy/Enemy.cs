using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : MonoBehaviour,
        IGameFixedUpdateListener
    {
        private EnemyAttackAgent attackAgent;
        private EnemyMoveAgent moveAgent;
        private EnemyDeathAgent deathAgent;
        private EnemyManager enemyManager;

        private HealthComponent healthComponent;
        private int initialHealth;

        [Inject]
        private void Construct(EnemyManager enemyManager)
        {
            this.enemyManager = enemyManager;
        }

        private void Awake()
        {
            this.attackAgent = this.GetComponent<EnemyAttackAgent>();
            this.moveAgent = this.GetComponent<EnemyMoveAgent>();
            this.deathAgent = this.GetComponent<EnemyDeathAgent>();
            deathAgent.Construct(enemyManager);

            this.healthComponent = this.GetComponent<HealthComponent>();
            this.initialHealth = healthComponent.Health;
        }

        public void Enable()
        {
            this.moveAgent.Enable();
            this.deathAgent.Enable();

            this.healthComponent.Health = this.initialHealth;
            this.moveAgent.OnTargetReached += this.OnTargetReached;
        }

        public void Disable()
        {
            this.deathAgent.Disable();

            if (this.moveAgent.enabled)
            {
                this.moveAgent.Disable();
            }

            if (this.attackAgent.enabled)
            {
                this.attackAgent.Disable();
            }

            this.moveAgent.OnTargetReached -= this.OnTargetReached;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (this.moveAgent.enabled)
            {
                this.moveAgent.OnFixedUpdate(deltaTime);
            }

            if (this.attackAgent.enabled)
            {
                this.attackAgent.OnFixedUpdate(deltaTime);
            }
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
            this.moveAgent.Disable();
            this.attackAgent.Enable();
        }
    }
}