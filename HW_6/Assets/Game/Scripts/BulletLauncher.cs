using DI;
using UnityEngine;

namespace Game
{
    public sealed class BulletLauncher
    {
        private BulletSystem bulletSystem;
        
        [Inject]
        private void Construct(BulletSystem bulletSystem)
        {
            this.bulletSystem = bulletSystem;
        }

        public void LaunchBullet(Vector3 position, Quaternion rotation, Vector3 direction)
        {
            Bullet bullet = bulletSystem.SpawnBullet();
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.MoveDirection.Value = direction;
        }
    }
}