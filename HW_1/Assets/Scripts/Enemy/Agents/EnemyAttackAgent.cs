using System;
using ShootEmUp.Components;
using UnityEngine;

namespace ShootEmUp
{
    namespace Enemy
    {
        
        [RequireComponent(typeof(ShootComponent),typeof(WeaponComponent))]
         public sealed class EnemyAttackAgent : MonoBehaviour
         {
             private WeaponComponent weaponComponent; 
             private ShootComponent shootComponent;
            public event Action FireEvent;

            [SerializeField] private float cooldown;

            private GameObject target;
            private float currentTime;

            private void Awake()
            {
                this.weaponComponent = this.GetComponent<WeaponComponent>();
                this.shootComponent = this.GetComponent<ShootComponent>();
            }
            
            private void OnEnable()
            {
                this.FireEvent += this.shootComponent.OnFireBullet;
            }
            
            private void FixedUpdate()
            {

                this.currentTime -= Time.fixedDeltaTime;
                if (this.currentTime > 0)
                {
                    return;
                }
                
                var vector = (Vector2)this.target.transform.position - this.weaponComponent.Position;
                var direction = vector.normalized;

                this.shootComponent.Direction = direction;
                this.FireEvent?.Invoke();
                this.Reset();
            }

            
            private void OnDisable()
            {
                this.FireEvent -= this.shootComponent.OnFireBullet;
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
        }
    }
}