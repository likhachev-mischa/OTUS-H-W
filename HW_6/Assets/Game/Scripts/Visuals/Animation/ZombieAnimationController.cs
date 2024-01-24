using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public enum ZombieStates
    {
        IDLE = 0,
        MOVING,
        DEAD
    };

    public class ZombieAnimatorController
    {
        private readonly Animator animator;

        private static readonly int MainState = Animator.StringToHash("MainState");
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");

        private readonly IAtomicValue<Vector3> moveDirection;
        private readonly IAtomicValue<bool> isDead;
        private readonly AtomicVariable<bool> canAttack;
        private readonly AtomicEvent attackRequest;
        private readonly IAtomicEvent<MonoBehaviour> despawn;
        private Transform transform;

        private CapsuleCollider capsuleCollider;


        public ZombieAnimatorController(Animator animator, IAtomicValue<Vector3> moveDirection,
            IAtomicValue<bool> isDead, AtomicEvent attackRequest, Transform transform, AtomicVariable<bool> canAttack,
            IAtomicEvent<MonoBehaviour> despawn)
        {
            this.animator = animator;
            this.moveDirection = moveDirection;
            this.isDead = isDead;
            this.attackRequest = attackRequest;
            this.canAttack = canAttack;
            this.despawn = despawn;
            this.transform = transform;
        }

        public void OnEnable()
        {
            attackRequest.Subscribe(OnAttackRequested);
            despawn.Subscribe(OnDespawn);
        }

        public void OnDisable()
        {
            attackRequest.Unsubscribe(OnAttackRequested);
            despawn.Unsubscribe(OnDespawn);
        }

        private void OnAttackRequested()
        {
            if (!canAttack.Value)
            {
                return;
            }

            animator.SetTrigger(AttackTrigger);
            canAttack.Value = false;
        }

        private async void OnDespawn(MonoBehaviour _)
        {
            GameObject visuals = Object.Instantiate(animator.gameObject, transform.position, transform.rotation); 
            visuals.GetComponent<Animator>().SetInteger(MainState, (int)ZombieStates.DEAD + 1);
            var millisecondsDelay = 10000;
            await UniTask.Delay(millisecondsDelay);
            Object.Destroy(visuals);
        }

        public void Update()
        {
            ZombieStates state = GetAnimationValue();
            animator.SetInteger(MainState, (int)state);
        }

        private ZombieStates GetAnimationValue()
        {
            if (isDead.Value)
            {
                return ZombieStates.DEAD;
            }

            if (moveDirection.Value != Vector3.zero)
            {
                return ZombieStates.MOVING;
            }

            return ZombieStates.IDLE;
        }
    }
}