using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour,
        IGameUpdateListener
    {
        [SerializeField] private EnemyManager enemyManager;

        [SerializeField] private float spawnDelay;

        private float timeElapsed;

        private void Awake()
        {
            Reset();
        }

        private void Reset()
        {
            timeElapsed = spawnDelay;
        }

        public void OnUpdate(float deltaTime)
        {
            timeElapsed -= deltaTime;
            if (timeElapsed > 0)
            {
                return;
            }

            this.Reset();
            enemyManager.SpawnEnemy();
        }
    }
}