using System;
using ShootEmUp.Components;
using UnityEngine;

namespace ShootEmUp
{
    namespace Enemy
    {
         public sealed class EnemyAttackAgent : EnemyAgent, IDependsOnReachedTarget, IShooter
        {

            [SerializeField] private Transform weaponTransform;

            public event Action ShootEvent;

            private BulletFactory bulletFactory;
            [SerializeField] private BulletConfig bulletConfig;

            private ShootComponent shootComponent;

            [SerializeField] private float cooldown;

            private GameObject target;
            private float currentTime;


            private void Awake()
            {
                bulletFactory = FindObjectOfType<BulletFactory>();
                shootComponent = new ShootComponent(this, weaponTransform,
                    bulletFactory, bulletConfig, Vector3.down, false);
            }

            private void OnEnable()
            {
                shootComponent.Enable();
            }


            public void SetTarget(GameObject target)
            {
                this.target = target;
            }

            public void Reset()
            {
                this.currentTime = this.cooldown;
            }

            public void OnTargetReached()
            {
                this.enabled = true;
            }

            private void FixedUpdate()
            {

                this.currentTime -= Time.fixedDeltaTime;
                if (this.currentTime > 0)
                {
                    return;
                }

                this.Fire();
                this.Reset();
            }

            private void Fire()
            {
                shootComponent.WeaponTransform = weaponTransform;
                Vector2 startPosition = this.weaponTransform.position;
                var vector = (Vector2)this.target.transform.position - startPosition;
                var direction = vector.normalized;
                shootComponent.UpdateDirection(direction);
                ShootEvent?.Invoke();
            }

            private void OnDisable()
            {
                shootComponent.Disable();
            }
        }
    }
}