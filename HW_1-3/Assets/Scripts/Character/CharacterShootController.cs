using UnityEngine;

namespace ShootEmUp
{
    public class CharacterShootController : MonoBehaviour,
        IGameStartListener,
        IGameFinishListener
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private GameObject character;

        private ShootComponent shootComponent;

        private void Awake()
        {
            this.shootComponent = this.character.GetComponent<ShootComponent>();
            this.shootComponent.Direction = Vector2.up;
        }

        public void OnStart()
        {
            this.inputManager.FireEvent += this.shootComponent.OnFireBullet;
        }

        
        public void OnFinish()
        {
            this.inputManager.FireEvent -= this.shootComponent.OnFireBullet;
        }
    }
}