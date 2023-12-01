using UnityEngine;

namespace ShootEmUp
{
    public class EnemyInstaller : GameInstaller
    {
        [Service(typeof(EnemyManager))] [Listener] [SerializeField]
        private EnemyManager enemyManager;

        [Listener] [SerializeField] private EnemySpawner enemySpawner;
    }
}