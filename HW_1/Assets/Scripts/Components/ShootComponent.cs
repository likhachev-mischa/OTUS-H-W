using System;
using UnityEngine;

namespace ShootEmUp
{
    namespace Components
    {
         public interface IShooter
        {
            public event Action ShootEvent;
        }

        public class ShootComponent
        {
            private readonly BulletFactory bulletFactory;
            private readonly BulletConfig bulletConfig;
            private WeaponComponent weaponComponent;

            private readonly Transform weaponTransform;
            private Vector2 direction;

            public Transform WeaponTransform { get; set; }

            private readonly bool isPlayer;

            private IShooter shooter;

            public ShootComponent(IShooter shooter, Transform weaponTransform,
                BulletFactory bulletFactory, BulletConfig bulletConfig, Vector2 direction, bool isPlayer = false)
            {
                this.shooter = shooter;
                this.weaponTransform = weaponTransform;
                this.bulletFactory = bulletFactory;
                this.bulletConfig = bulletConfig;
                this.direction = direction;
                this.isPlayer = isPlayer;
            }

            public void Enable()
            {
                shooter.ShootEvent += OnBulletShot;
            }

            public void UpdateDirection(Vector2 direction)
            {
                this.direction = direction;
            }

            public void Disable()
            {
                shooter.ShootEvent -= OnBulletShot;
            }


            private void OnBulletShot()
            {
                BulletLauncherInteractor.LaunchBullet(new Bullet.Args
                {
                    isPlayer = this.isPlayer,
                    physicsLayer = (int)this.bulletConfig.physicsLayer,
                    color = this.bulletConfig.color,
                    damage = this.bulletConfig.damage,
                    position = weaponTransform.position,
                    velocity = weaponTransform.rotation * direction * this.bulletConfig.speed,
                }, bulletFactory);
            }
        }
    }
}