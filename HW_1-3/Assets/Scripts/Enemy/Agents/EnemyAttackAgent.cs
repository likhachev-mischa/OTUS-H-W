using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(ShootComponent), typeof(WeaponComponent))]
    public sealed class EnemyAttackAgent : MonoBehaviour,
        IGameFixedUpdateListener
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

        public void Enable()
        {
            this.enabled = true;
            this.Reset();
            this.FireEvent += this.shootComponent.OnFireBullet;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            this.currentTime -= deltaTime;
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

        public void Disable()
        {
            this.enabled = false;
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
    }
}