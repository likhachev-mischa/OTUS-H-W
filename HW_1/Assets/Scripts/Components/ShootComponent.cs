using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IShooter
    {
        public event Action ShootEvent;
    }
    public class ShootComponent
    {
        private readonly BulletSystem bulletSystem;
        private readonly BulletConfig bulletConfig;
        private WeaponComponent weaponComponent;

        private readonly Transform weaponTransform;
        
        public Transform WeaponTransform { get; set; }

        private readonly bool isPlayer;

        private IShooter shooter;
        public ShootComponent(IShooter shooter, Transform weaponTransform, 
            BulletSystem bulletSystem, BulletConfig bulletConfig, bool isPlayer = false)
        {
            this.shooter = shooter;
            this.weaponTransform = weaponTransform;
            this.bulletSystem = bulletSystem;
            this.bulletConfig = bulletConfig;
            this.isPlayer = isPlayer;
        }
        public void Enable()
        {
            shooter.ShootEvent += OnBulletShot;
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
                physicsLayer = (int) this.bulletConfig.physicsLayer,
                color = this.bulletConfig.color,
                damage = this.bulletConfig.damage,
                position = weaponTransform.position,
                velocity = weaponTransform.rotation * Vector3.up * this.bulletConfig.speed,
            }, bulletSystem);
        }
    }
}