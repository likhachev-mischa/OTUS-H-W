using UnityEngine;

namespace ShootEmUp
{
    public class CharacterShootController : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private GameObject character;

        private ShootComponent shootComponent;

        private void Awake()
        {
            this.shootComponent = this.character.GetComponent<ShootComponent>();
            this.shootComponent.Direction = Vector2.up;
        }

        private void OnEnable()
        {
            this.inputManager.FireEvent += this.shootComponent.OnFireBullet;
        }

        private void OnDisable()
        {
            this.inputManager.FireEvent -= this.shootComponent.OnFireBullet;
        }
    }
}