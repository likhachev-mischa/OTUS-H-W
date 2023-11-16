
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public class CharacterSystem : MonoBehaviour
    {
        [SerializeField] private float speed;

        [SerializeField] private InputManager inputManager;
        [SerializeField] private Transform weaponTransform;

        [SerializeField] private LevelBounds levelBounds;
        
        public BulletSystem bulletSystem;
        public BulletConfig bulletConfig;

        private MoveComponent moveComponent;
        private ShootComponent shootComponent;
        private ContainInBoundsInteractor containInBoundsInteractor;

        private void Awake()
        {
            moveComponent = new MoveComponent(this.GetComponent<Rigidbody2D>(), speed);
            shootComponent = new ShootComponent(inputManager, weaponTransform,
                bulletSystem, bulletConfig, true);
            containInBoundsInteractor = new ContainInBoundsInteractor(levelBounds, this.transform.position);
        }


        private void FixedUpdate()
        {
            Vector2 direction = new Vector2(inputManager.HorizontalDirection, 0);
            containInBoundsInteractor.CorrectDirection(ref direction, this.transform.position);
            moveComponent.MoveByRigidbodyVelocity(direction * Time.fixedDeltaTime);
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