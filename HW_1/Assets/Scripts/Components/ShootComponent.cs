
using UnityEngine;

namespace ShootEmUp
{
    namespace Components
    {
         
        [RequireComponent(typeof(TeamComponent),typeof(WeaponComponent))]
        public class ShootComponent : MonoBehaviour
        {
            [SerializeField]
            private BulletConfig bulletConfig;
            
            private BulletLauncher bulletLauncher;
            
            private WeaponComponent weaponComponent;
            private TeamComponent teamComponent;
            
            public Vector2 Direction { get; set; }
            
            private void Awake()
            {
                this.bulletLauncher = FindObjectOfType<BulletLauncher>();
                
                this.weaponComponent = this.GetComponent<WeaponComponent>();
                this.teamComponent = this.GetComponent<TeamComponent>();
            }
            
            public void OnFireBullet()
            {
                bulletLauncher.LaunchBullet(new Bullet.Args
                {
                    isPlayer = this.teamComponent.IsPlayer,
                    physicsLayer = (int)this.bulletConfig.physicsLayer,
                    color = this.bulletConfig.color,
                    damage = this.bulletConfig.damage,
                    position = this.weaponComponent.Position,
                    velocity = this.weaponComponent.Rotation * this.Direction * this.bulletConfig.speed
                });
            }
        }
    }
}