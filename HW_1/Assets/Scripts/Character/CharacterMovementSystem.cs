using ShootEmUp.Components;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterMovementSystem : MonoBehaviour
    {
        [SerializeField] private float speed;

        [SerializeField] private InputManager inputManager;

        [SerializeField] private LevelBounds levelBounds;

        private MoveComponent moveComponent;
        private ContainInBoundsInteractor containInBoundsInteractor;

        private void Awake()
        {
            moveComponent = new MoveComponent(this.GetComponent<Rigidbody2D>(), speed);

            containInBoundsInteractor = new ContainInBoundsInteractor(levelBounds, this.transform.position);
        }
        
        
        private void FixedUpdate()
        {
            Vector2 direction = new Vector2(inputManager.HorizontalDirection, 0);
            containInBoundsInteractor.CorrectDirection(ref direction, this.transform.position);
            
            moveComponent.MoveByRigidbodyVelocity(direction * Time.fixedDeltaTime);
            
        }

    }

}