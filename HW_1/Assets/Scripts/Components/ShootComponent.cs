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
        private BulletManager bulletManager;
        private BulletConfig bulletConfig;
        private WeaponComponent weaponComponent;

        private Transform weaponTransform;
        
        public Transform WeaponTransform { get; set; }

        private bool isPlayer;

        private IShooter shooter;
        public ShootComponent(IShooter shooter, Transform weaponTransform, 
            BulletManager bulletManager, BulletConfig bulletConfig, bool isPlayer = false)
        {
            this.shooter = shooter;
            this.weaponTransform = weaponTransform;
            this.bulletManager = bulletManager;
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
        
        
        public void OnBulletShot()
        {
            bulletManager.LaunchBullet(new BulletManager.Args
            {
                isPlayer = this.isPlayer,
                physicsLayer = (int) this.bulletConfig.physicsLayer,
                color = this.bulletConfig.color,
                damage = this.bulletConfig.damage,
                position = weaponTransform.position,
                velocity = weaponTransform.rotation * Vector3.up * this.bulletConfig.speed
            });
        }
    }
}