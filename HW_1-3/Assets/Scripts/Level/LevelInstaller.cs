using UnityEngine;

namespace ShootEmUp
{
    public class LevelInstaller : GameInstaller
    {
        [Service(typeof(LevelBounds))] [SerializeField]
        private LevelBounds levelBounds;

        [Service(typeof(EnemyPositions))] [SerializeField]
        private EnemyPositions enemyPositions;

        [Service(typeof(World))] [SerializeField]
        private World world;

        [Listener] [SerializeField] private LevelBackground levelBackground;
    }
}