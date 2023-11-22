using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour
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

        private void Update()
        {
            timeElapsed -= Time.deltaTime;
            if (timeElapsed > 0)
            {
                return;
            }

            this.Reset();
            enemyManager.SpawnEnemy();
        }
    }
}