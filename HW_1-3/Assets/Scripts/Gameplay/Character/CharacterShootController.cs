using UnityEngine;

namespace ShootEmUp
{
    public class CharacterShootController :
        IGameStartListener,
        IGameFinishListener
    {
        private InputManager inputManager;
        private ShootComponent shootComponent;

        [Inject]
        private void Construct(Character character, InputManager inputManager)
        {
            this.shootComponent = character.GetComponent<ShootComponent>();
            this.inputManager = inputManager;
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