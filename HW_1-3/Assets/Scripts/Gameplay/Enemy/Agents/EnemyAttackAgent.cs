using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(ShootComponent), typeof(WeaponComponent))]
    public sealed class EnemyAttackAgent : 
        MonoBehaviour,
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
            weaponComponent = GetComponent<WeaponComponent>();
            shootComponent = GetComponent<ShootComponent>();
        }

        public void Enable()
        {
            enabled = true;
            Reset();
            FireEvent += shootComponent.OnFireBullet;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            currentTime -= deltaTime;
            if (currentTime > 0)
            {
                return;
            }

            Vector2 vector = (Vector2)target.transform.position - weaponComponent.Position;
            Vector2 direction = vector.normalized;

            shootComponent.Direction = direction;
            FireEvent?.Invoke();
            Reset();
        }

        public void Disable()
        {
            enabled = false;
            FireEvent -= shootComponent.OnFireBullet;
        }

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        public void Reset()
        {
            currentTime = cooldown;
        }
    }
}