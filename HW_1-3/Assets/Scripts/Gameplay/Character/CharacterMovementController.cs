using UnityEngine;

namespace ShootEmUp

{
    public class CharacterMovementController :
        IGameFixedUpdateListener
    {
        private InputManager inputManager;
        private MoveComponent moveComponent;
        private Character character;
        private ContainInBoundsCorrector containInBoundsCorrector;

        [Inject]
        private void Construct(InputManager inputManager, Character character,
            ContainInBoundsCorrector containInBoundsCorrector)
        {
            this.inputManager = inputManager;
            this.character = character;
            moveComponent = character.GetComponent<MoveComponent>();
            this.containInBoundsCorrector = containInBoundsCorrector;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            var direction = new Vector2(inputManager.MoveDirection, 0);

            containInBoundsCorrector.CorrectDirection(ref direction, character.transform.position);
            moveComponent.Move(direction * deltaTime);
        }
    }
}