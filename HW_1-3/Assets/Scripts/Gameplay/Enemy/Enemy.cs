using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : MonoBehaviour,
        IGameFixedUpdateListener
    {
        public event Action<Enemy> OnDeath
        {
            add => deathAgent.OnDeath += value;
            remove => deathAgent.OnDeath -= value;
        }

        private EnemyAttackAgent attackAgent;
        private EnemyMoveAgent moveAgent;
        private EnemyDeathAgent deathAgent;

        private HealthComponent healthComponent;
        private int initialHealth;
        
        private void Awake()
        {
            attackAgent = GetComponent<EnemyAttackAgent>();
            moveAgent = GetComponent<EnemyMoveAgent>();
            deathAgent = GetComponent<EnemyDeathAgent>();

            healthComponent = GetComponent<HealthComponent>();
            initialHealth = healthComponent.Health;
        }

        public void Enable()
        {
            moveAgent.Enable();
            deathAgent.Enable();

            healthComponent.Health = initialHealth;
            moveAgent.OnTargetReached += OnTargetReached;
        }

        public void Disable()
        {
            deathAgent.Disable();

            if (moveAgent.enabled)
            {
                moveAgent.Disable();
            }

            if (attackAgent.enabled)
            {
                attackAgent.Disable();
            }

            moveAgent.OnTargetReached -= OnTargetReached;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (moveAgent.enabled)
            {
                moveAgent.OnFixedUpdate(deltaTime);
            }

            if (attackAgent.enabled)
            {
                attackAgent.OnFixedUpdate(deltaTime);
            }
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetDestination(Vector2 destination)
        {
            moveAgent.SetDestination(destination);
        }

        public void SetTarget(GameObject target)
        {
            attackAgent.SetTarget(target);
        }

        private void OnTargetReached()
        {
            moveAgent.Disable();
            attackAgent.Enable();
        }
        
    }
}