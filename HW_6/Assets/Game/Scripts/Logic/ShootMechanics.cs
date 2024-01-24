using UnityEngine;

namespace Game
{
    public sealed class ShootMechanics
    {
        private readonly IAtomicEvent fireEvent;
        private readonly Transform firePoint;
        private readonly Transform transform;

        private readonly BulletLauncher bulletLauncher;

        public ShootMechanics(IAtomicEvent fireEvent, Transform firePoint,Transform transform, BulletLauncher bulletLauncher)
        {
            this.fireEvent = fireEvent;
            this.firePoint = firePoint;
            this.transform = transform;

            this.bulletLauncher = bulletLauncher;
        }

        public void OnEnable()
        {
            fireEvent.Subscribe(OnFire);
        }

        public void OnDisable()
        {
            fireEvent.Unsubscribe(OnFire);
        }

        private void OnFire()
        {
            bulletLauncher.LaunchBullet(firePoint.position,firePoint.rotation,transform.forward);
        }
    }
}