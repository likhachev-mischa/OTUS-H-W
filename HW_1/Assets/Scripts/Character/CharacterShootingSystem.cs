using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterShootingSystem : MonoBehaviour,IShooter
    {

        [SerializeField] private InputManager inputManager;
        [SerializeField] private Transform weaponTransform;

        public event Action ShootEvent;
        
        [SerializeField]
        private BulletSystem bulletSystem;
        [SerializeField]
        private BulletConfig bulletConfig;
        
        private ShootComponent shootComponent;

        private void Awake()
        {
            shootComponent = new ShootComponent(this, weaponTransform,
                bulletSystem, bulletConfig, true);
        }
        
        private void OnEnable()
        {
            shootComponent.Enable();
            inputManager.SpacePressedEvent += ShootEvent;
        }
        
        private void FixedUpdate()
        {
            shootComponent.WeaponTransform = weaponTransform;
        }

        private void OnDisable()
        {
            shootComponent.Disable();
            inputManager.SpacePressedEvent -= ShootEvent;
        }
        
    }
}