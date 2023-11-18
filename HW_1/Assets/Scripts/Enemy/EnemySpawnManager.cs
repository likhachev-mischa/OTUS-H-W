using UnityEngine;

namespace ShootEmUp
{
    namespace Enemy
    {
         public sealed class EnemySpawnManager : MonoBehaviour
        {

            [SerializeField] private EnemyFactory enemyFactory;
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
                enemyFactory.SpawnEnemy(out var enemy);
            }



        }
    }
}