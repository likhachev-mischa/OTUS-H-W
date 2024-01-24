using DI;
using UnityEngine;

namespace Game
{
    public sealed class Character : MonoBehaviour, IGamePostConstructListener, IGameUpdateListener
    {
        [Header("Stats")] public AtomicVariable<int> Health;
        public AtomicVariable<int> MoveSpeed;
        public AtomicVariable<int> RotationSpeed;
        public AtomicVariable<int> BulletCount;
        public AtomicVariable<float> BulletCooldown;

        [Header("Position")] public AtomicVariable<Vector3> MoveDirection;
        public AtomicVariable<Vector3> RotateDirection;

        [Header("Triggers")] public AtomicVariable<bool> IsDead;
        public AtomicVariable<bool> CanMove;
        public AtomicVariable<bool> CanShoot;

        [Header("Events")] public AtomicEvent Death;
        public AtomicEvent<Vector3> Moved;
        public AtomicEvent<Vector3> Rotated;
        public AtomicEvent FireRequest;
        public AtomicEvent FireEvent;
        public AtomicEvent<int> TakeDamage;

        public Transform FirePoint;

        private BulletLauncher bulletLauncher;

        private MovementMechanics movementMechanics;
        private RotationMechanics rotationMechanics;
        private ShootMechanics shootMechanics;
        private TakeDamageMechanics takeDamageMechanics;
        private DeathMechanics deathMechanics;

        private BulletCountMechanics bulletCountMechanics;
        private BulletRecoveryMechanics bulletRecoveryMechanics;
        private CanShootMechanics canShootMechanics;

        [Inject]
        private void Construct(BulletLauncher bulletLauncher)
        {
            this.bulletLauncher = bulletLauncher;
        }

        public void OnPostConstruct()
        {
            Transform characterTransform = this.transform;
            movementMechanics = new MovementMechanics(MoveSpeed, MoveDirection, Moved, characterTransform, CanMove);
            rotationMechanics =
                new RotationMechanics(RotationSpeed, RotateDirection, Rotated, characterTransform, CanMove);
            shootMechanics = new ShootMechanics(FireEvent, FirePoint, characterTransform, bulletLauncher);
            takeDamageMechanics = new TakeDamageMechanics(Health, TakeDamage, Death);
            deathMechanics = new DeathMechanics(IsDead, Death);

            bulletCountMechanics = new BulletCountMechanics(BulletCount, FireEvent);
            bulletRecoveryMechanics = new BulletRecoveryMechanics(BulletCooldown, BulletCount);
            canShootMechanics = new CanShootMechanics(IsDead, CanShoot, BulletCount);

            Enable();
        }

        private void Enable()
        {
            movementMechanics.OnEnable();
            rotationMechanics.OnEnable();
            shootMechanics.OnEnable();
            takeDamageMechanics.OnEnable();
            deathMechanics.OnEnable();
            
            bulletCountMechanics.OnEnable();
            canShootMechanics.OnEnable();
        }
        
        public void OnUpdate(float deltaTime)
        {
            bulletRecoveryMechanics.Update(deltaTime);
        }
       
        private void OnDisable()
        {
            movementMechanics.OnDisable();
            rotationMechanics.OnDisable();
            shootMechanics.OnDisable();
            takeDamageMechanics.OnDisable();
            deathMechanics.OnDisable();
            
            bulletCountMechanics.OnDisable();
            canShootMechanics.OnDisable();
        }

       
    }
}