using UnityEngine;

namespace ShootEmUp
{
    public class EnemyManager : IGameFixedUpdateListener
    {
        private int initialCount;
        private GameObject prefab;

        private Transform container;
        private Transform worldTransform;
        private EnemyPositions enemyPositions;
        private GameObject character;

        private ObjectPool<Enemy> enemyPool;

        [Inject]
        private void Construct(World world, EnemyPositions enemyPositions,
            Character character, EnemyManagerConfig config)
        {
            worldTransform = world.transform;
            this.enemyPositions = enemyPositions;
            this.character = character.gameObject;
            initialCount = config.initialCount;
            prefab = config.prefab;

            CreatePool();
        }

        private void CreatePool()
        {
            var pool = new GameObject("Enemy Pool");
            //pool.transform.SetParent(transform);
            pool.SetActive(false);
            container = pool.transform;
            enemyPool = new ObjectPool<Enemy>(initialCount, container,
                prefab, worldTransform);
            enemyPool.Initialize();
        }

        public void SpawnEnemy()
        {
            if (!enemyPool.SpawnObject(out Enemy enemy))
            {
                return;
            }

            enemy.SetPosition(enemyPositions.RandomSpawnPosition().position);
            enemy.Enable();
            enemy.OnDeath += DespawnEnemy;
            enemy.SetDestination(enemyPositions.RandomAttackPosition().position);
            enemy.SetTarget(character);
        }

        public void DespawnEnemy(Enemy enemy)
        {
            enemy.Disable();
            enemy.OnDeath -= DespawnEnemy;
            enemyPool.RemoveObject(enemy);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            for (var i = 0; i < enemyPool.ActiveObjects.Count; i++)
            {
                enemyPool.ActiveObjects[i].OnFixedUpdate(deltaTime);
            }
        }
    }
}