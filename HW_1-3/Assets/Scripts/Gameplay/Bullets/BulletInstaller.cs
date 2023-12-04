using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletInstaller : GameInstaller
    {
        [Service(typeof(BulletManagerConfig))] [SerializeField]
        private BulletManagerConfig bulletManagerConfig;

        [Service(typeof(BulletManager))] [Listener]
        private BulletManager bulletManager = new();

        [Service(typeof(BulletBoundsCorrector))] [Listener]
        private BulletBoundsCorrector bulletBoundsCorrector = new();

        [Service(typeof(BulletLauncher))] private BulletLauncher bulletLauncher = new();
    }
}