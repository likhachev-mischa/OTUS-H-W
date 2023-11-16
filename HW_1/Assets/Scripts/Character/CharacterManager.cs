
using System;
using UnityEngine;

namespace ShootEmUp
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private float speed;

        [SerializeField] private InputManager inputManager;
        [SerializeField] private Transform weaponTransform;

        public BulletManager bulletManager;
        public BulletConfig bulletConfig;

        private MoveComponent moveComponent;
        private ShootComponent shootComponent;

        private void Awake()
        {
            moveComponent = new MoveComponent(this.GetComponent<Rigidbody2D>(), speed);
            shootComponent = new ShootComponent(inputManager, weaponTransform,
                bulletManager, bulletConfig, true);
        }


        private void FixedUpdate()
        {
            moveComponent.MoveByRigidbodyVelocity(new Vector2(inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
            shootComponent.WeaponTransform = weaponTransform;
        }
        

        private void OnEnable()
        {
            shootComponent.Enable();
        }

        private void OnDisable()
        {
            shootComponent.Disable();
        }

        void Start()
        {
        
        }

    
        void Update()
        {
        
        }
    }

}