using UnityEngine;

namespace ShootEmUp

{
    public class CharacterMovementController : MonoBehaviour,
        IGameFixedUpdateListener
    {
        [SerializeField] private InputManager inputManager;

        [SerializeField] private GameObject character;

        private MoveComponent moveComponent;
        private ContainInBoundsCorrector containInBoundsCorrector;

        private void Awake()
        {
            this.containInBoundsCorrector = FindObjectOfType<ContainInBoundsCorrector>();
            this.moveComponent = this.character.GetComponent<MoveComponent>();
        }

        public void OnFixedUpdate(float deltaTime)
        {
            Vector2 direction = new Vector2(this.inputManager.MoveDirection, 0);

            this.containInBoundsCorrector.CorrectDirection(ref direction, this.moveComponent.transform.position);
            this.moveComponent.Move(direction * deltaTime);
        }
    }
}