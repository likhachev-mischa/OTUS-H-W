﻿using UnityEngine;

namespace Game
{
    public enum CharacterStates
    {
        IDLE = 0,
        MOVING_BACKWARD,
        MOVING_BACKWARD_LEFT,
        MOVING_BACKWARD_RIGHT,
        MOVING_FORWARD,
        MOVING_FORWARD_LEFT,
        MOVING_FORWARD_RIGHT,
        MOVING_LEFT,
        MOVING_RIGHT,
        DEAD
    };

    public class CharacterAnimatorController
    {
        private readonly Animator animator;

        private static readonly int MainState = Animator.StringToHash("MainState");
        private static readonly int ShootTrigger = Animator.StringToHash("Shoot");

        private readonly IAtomicValue<Vector3> moveDirection;
        private readonly IAtomicValue<bool> isDead;
        private readonly AtomicVariable<bool> canShoot;
        private readonly AtomicEvent fireRequest;
        private readonly Transform transform;
        private readonly AtomicVariable<bool> canPlayAnimation;

        public CharacterAnimatorController(Animator animator, IAtomicValue<Vector3> moveDirection,
            IAtomicValue<bool> isDead, AtomicEvent fireRequest, Transform transform, AtomicVariable<bool> canShoot,
            AtomicVariable<bool> canPlayAnimation)
        {
            this.animator = animator;
            this.moveDirection = moveDirection;
            this.isDead = isDead;
            this.fireRequest = fireRequest;
            this.transform = transform;
            this.canShoot = canShoot;
            this.canPlayAnimation = canPlayAnimation;
        }

        public void OnEnable()
        {
            fireRequest.Subscribe(OnFireRequested);
        }

        public void OnDisable()
        {
            fireRequest.Unsubscribe(OnFireRequested);
        }

        private void OnFireRequested()
        {
            if (!canShoot.Value || !canPlayAnimation.Value)
            {
                return;
            }
            animator.SetTrigger(ShootTrigger);
            canPlayAnimation.Value = false;
        }

        public void Update()
        {
            CharacterStates state = GetAnimationValue();
            animator.SetInteger(MainState, (int)state);
        }

        private CharacterStates GetAnimationValue()
        {
            if (isDead.Value)
            {
                return CharacterStates.DEAD;
            }

            if (moveDirection.Value != Vector3.zero)
            {
                return GetMovementDirection();
            }
            
            return CharacterStates.IDLE;
        }

        private CharacterStates GetMovementDirection()
        {
            float x = moveDirection.Value.x;
            float z = moveDirection.Value.z;

            int movementAngle = (int)(Mathf.Atan2(x, z) * Mathf.Rad2Deg);

            Vector3 currentRotation = transform.rotation.eulerAngles;

            Vector2 normalVector = new(Mathf.Sin(currentRotation.y * Mathf.Deg2Rad),
                Mathf.Cos(currentRotation.y * Mathf.Deg2Rad));

            int characterAngle = (int)(Mathf.Atan2(normalVector.x, normalVector.y) * Mathf.Rad2Deg);

            characterAngle = SnapAngle(characterAngle);

            int resultAngle = movementAngle - characterAngle;

            if (resultAngle <= -180)
            {
                resultAngle += 360;
            }
            else if (resultAngle >= 180)
            {
                resultAngle -= 360;
            }

            return resultAngle switch
            {
                45 => CharacterStates.MOVING_FORWARD_RIGHT,
                -45 => CharacterStates.MOVING_FORWARD_LEFT,
                0 => CharacterStates.MOVING_FORWARD,
                135 => CharacterStates.MOVING_BACKWARD_RIGHT,
                -135 => CharacterStates.MOVING_BACKWARD_LEFT,
                90 => CharacterStates.MOVING_RIGHT,
                -90 => CharacterStates.MOVING_LEFT,
                _ => CharacterStates.MOVING_BACKWARD
            };
        }

        private int SnapAngle(int angle)
        {
            return angle switch
            {
                >= -22 and < 22 => 0,
                >= 22 and < 67 => 45,
                >= 67 and < 112 => 90,
                >= 112 and < 157 => 135,
                >= -157 and < -112 => -135,
                >= -112 and < -67 => -90,
                >= -67 and < -22 => -45,
                _ => 180
            };
        }
    }
}