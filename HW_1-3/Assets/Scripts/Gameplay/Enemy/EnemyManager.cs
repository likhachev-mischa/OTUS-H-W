using UnityEngine;

namespace ShootEmUp
{
    public class EnemyManager : MonoBehaviour,
        IGameFixedUpdateListener
    {
        [SerializeField] private int initialCount = 7;
        [SerializeField] private ServiceLocator serviceLocator;
        [SerializeField] private GameObject prefab;

        private Transform container;
        private Transform worldTransform;
        private EnemyPositions enemyPositions;
        private GameObject character;

        private ObjectPool<Enemy> enemyPool;

        [Inject]
        private void Construct(World world, EnemyPositions enemyPositions,
            Character character)
        {
            this.worldTransform = world.transform;
            this.enemyPositions = enemyPositions;
            this.character = character.gameObject;
        }

        private void Awake()
        {
            var pool = new GameObject("Enemy Pool");
            pool.transform.SetParent(this.transform);
            pool.SetActive(false);
            this.container = pool.transform;
            enemyPool = new ObjectPool<Enemy>(initialCount, container, prefab,
                worldTransform, serviceLocator);
            enemyPool.Initialize();
        }

        public void SpawnEnemy()
        {
            if (!enemyPool.SpawnObject(out var enemy))
            {
                return;
            }

            enemy.SetPosition(this.enemyPositions.RandomSpawnPosition().position);
            enemy.Enable();
            enemy.SetDestination(this.enemyPositions.RandomAttackPosition().position);
            enemy.SetTarget(this.character);
        }

        public void DespawnEnemy(Enemy enemy)
        {
            enemy.Disable();
            enemyPool.RemoveObject(enemy);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            for (var i = 0; i < this.enemyPool.ActiveObjects.Count; i++)
            {
                this.enemyPool.ActiveObjects[i].OnFixedUpdate(deltaTime);
            }
        }
    }
}