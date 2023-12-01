using UnityEngine;

namespace ShootEmUp
{
    public class BulletInstaller : GameInstaller
    {
        [Service(typeof(BulletManager))] [Listener] [SerializeField]
        private BulletManager bulletManager;

        [Service(typeof(BulletBoundsCorrector))] [Listener]
        private BulletBoundsCorrector bulletBoundsCorrector = new();

        [Service(typeof(BulletLauncher))] private BulletLauncher bulletLauncher = new();
    }
}