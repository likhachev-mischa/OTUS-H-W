using System;
using ShootEmUp.Components;
using UnityEngine;

namespace ShootEmUp

{
    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;

        [SerializeField]
        private GameObject character;

        private MoveComponent moveComponent;
        private ContainInBoundsController containInBoundsController;

        private void Awake()
        {
            this.containInBoundsController = FindObjectOfType<ContainInBoundsController>();
            this.moveComponent = this.character.GetComponent<MoveComponent>();
        }

        private void FixedUpdate()
        {
            Vector2 direction = new Vector2(this.inputManager.MoveDirection, 0);
            
            this.containInBoundsController.CorrectDirection(ref direction, this.moveComponent.transform.position);
            this.moveComponent.Move(direction * Time.fixedDeltaTime);

        }
    }
}