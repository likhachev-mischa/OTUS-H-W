namespace ShootEmUp
{
    public sealed class EnemySpawner : IGameUpdateListener
    {
        private float spawnDelay;
        private EnemyManager enemyManager;
        private float timeElapsed;

        [Inject]
        private void Construct(EnemyManager enemyManager, EnemyManagerConfig config)
        {
            this.enemyManager = enemyManager;
            spawnDelay = config.spawnDelay;
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

            Reset();
            enemyManager.SpawnEnemy();
        }
    }
}