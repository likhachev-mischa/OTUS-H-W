using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : MonoBehaviour
    {
        private EnemyAttackAgent enemyAttackAgent;
        private EnemyMoveAgent enemyMoveAgent;

        private HealthComponent healthComponent;
        private int initialHealth;

        private void Awake()
        {
            this.enemyAttackAgent = this.GetComponent<EnemyAttackAgent>();
            this.enemyMoveAgent = this.GetComponent<EnemyMoveAgent>();

            this.healthComponent = this.GetComponent<HealthComponent>();
            this.initialHealth = healthComponent.Health;
        }

        public void Enable()
        {
            this.enemyAttackAgent.enabled = false;
            this.enemyMoveAgent.enabled = true;
            this.healthComponent.Health = initialHealth;

            this.enemyMoveAgent.TargetReachedEvent += OnTargetReached;
        }

        public void Disable()
        {
            this.enemyAttackAgent.enabled = false;
            this.enemyMoveAgent.enabled = false;

            this.enemyMoveAgent.TargetReachedEvent -= OnTargetReached;
        }

        public void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        public void SetDestination(Vector2 destination)
        {
            this.enemyMoveAgent.SetDestination(destination);
        }

        public void SetTarget(GameObject target)
        {
            this.enemyAttackAgent.SetTarget(target);
        }

        private void OnTargetReached()
        {
            this.enemyMoveAgent.enabled = false;
            this.enemyAttackAgent.enabled = true;
        }
    }
}