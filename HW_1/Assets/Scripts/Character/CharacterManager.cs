
using UnityEngine;

namespace ShootEmUp
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private float speed;

        [SerializeField] public InputManager inputManager;
    
        private MoveComponent moveComponent;

        private void Awake()
        {
            moveComponent = new MoveComponent(this.GetComponent<Rigidbody2D>(), speed);
        }


        private void FixedUpdate()
        {
            moveComponent.MoveByRigidbodyVelocity(new Vector2(inputManager.HorizontalDirection, 0) * Time.fixedDeltaTime);
        }

        void Start()
        {
        
        }

    
        void Update()
        {
        
        }
    }

}