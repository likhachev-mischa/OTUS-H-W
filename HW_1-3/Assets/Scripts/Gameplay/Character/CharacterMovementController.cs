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
            this.moveComponent = character.GetComponent<MoveComponent>();
            this.containInBoundsCorrector = containInBoundsCorrector;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            Vector2 direction = new Vector2(this.inputManager.MoveDirection, 0);

            this.containInBoundsCorrector.CorrectDirection(ref direction, this.character.transform.position);
            this.moveComponent.Move(direction * deltaTime);
        }
    }
}