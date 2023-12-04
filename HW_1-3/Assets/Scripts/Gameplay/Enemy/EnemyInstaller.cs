using UnityEngine;

namespace ShootEmUp
{
    public class EnemyInstaller : GameInstaller
    {
        [Service(typeof(EnemyManagerConfig))] [SerializeField]
        private EnemyManagerConfig enemyManagerConfig;

        [Service(typeof(EnemyManager))] [Listener]
        private EnemyManager enemyManager = new();

        [Listener] private EnemySpawner enemySpawner = new();
    }
}