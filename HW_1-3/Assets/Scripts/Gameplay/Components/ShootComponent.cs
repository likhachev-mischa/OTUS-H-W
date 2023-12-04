using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(TeamComponent), typeof(WeaponComponent))]
    public class ShootComponent : MonoBehaviour
    {
        [SerializeField] private BulletConfig bulletConfig;

        private BulletLauncher bulletLauncher;
        private WeaponComponent weaponComponent;
        private TeamComponent teamComponent;

        public Vector2 Direction { get; set; }

        [Inject]
        private void Construct(BulletLauncher bulletLauncher)
        {
            this.bulletLauncher = bulletLauncher;
        }

        private void Awake()
        {
            weaponComponent = GetComponent<WeaponComponent>();
            teamComponent = GetComponent<TeamComponent>();
        }

        public void OnFireBullet()
        {
            bulletLauncher.LaunchBullet(new Bullet.Args
            {
                isPlayer = teamComponent.IsPlayer,
                physicsLayer = (int)bulletConfig.physicsLayer,
                color = bulletConfig.color,
                damage = bulletConfig.damage,
                position = weaponComponent.Position,
                velocity = weaponComponent.Rotation * Direction * bulletConfig.speed
            });
        }
    }
}