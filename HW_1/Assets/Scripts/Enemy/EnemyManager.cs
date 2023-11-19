using ShootEmUp.Components;
using UnityEngine;

namespace ShootEmUp
{
    namespace Enemy
    {
         public class EnemyManager : MonoBehaviour
        {
            [SerializeField] private int initialCount = 7;

            [SerializeField] private Transform container;
            [SerializeField] private GameObject prefab;
            [SerializeField] private Transform worldTransform;

            [SerializeField] private EnemyPositions enemyPositions;
            [SerializeField] private GameObject character;

            private ObjectPool<EnemyFacade> enemyPool;

            private int initialHealth;
            
            private void Awake()
            {
                enemyPool = new ObjectPool<EnemyFacade>(initialCount, container, prefab, worldTransform);
                enemyPool.Initialize();

                this.initialHealth = prefab.GetComponent<HealthComponent>().Health;
            }

            public void SpawnEnemy()
            {
                if (!enemyPool.SpawnObject(out var enemy))
                {
                    return;
                }

                var spawnPosition = this.enemyPositions.RandomSpawnPosition();
                enemy.transform.position = spawnPosition.position;

                enemy.enemyAttackAgent.enabled = false;
                enemy.enemyMoveAgent.enabled = true;
                
                enemy.enemyDeathAgent.healthComponent.Health = initialHealth;
                enemy.enemyMoveAgent.SetDestination(enemyPositions.RandomAttackPosition().position);
                enemy.enemyMoveAgent.TargetReachedEvent += OnTargetReached;

            }
            
            public void DespawnEnemy(EnemyFacade enemy)
            {
                enemy.enemyAttackAgent.enabled = false;
                enemy.enemyMoveAgent.enabled = false;

                enemy.enemyMoveAgent.TargetReachedEvent -= OnTargetReached;
                enemyPool.RemoveObject(enemy);
            }

            private void OnTargetReached(EnemyFacade enemy)
            {
                enemy.enemyMoveAgent.enabled = false;
                enemy.enemyAttackAgent.enabled = true;
                enemy.enemyAttackAgent.SetTarget(character);
            }

           
        }
    }
}