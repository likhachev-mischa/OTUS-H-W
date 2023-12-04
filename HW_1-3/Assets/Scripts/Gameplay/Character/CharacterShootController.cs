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
            shootComponent = character.GetComponent<ShootComponent>();
            this.inputManager = inputManager;
            shootComponent.Direction = Vector2.up;
        }

        public void OnStart()
        {
            inputManager.FireEvent += shootComponent.OnFireBullet;
        }


        public void OnFinish()
        {
            inputManager.FireEvent -= shootComponent.OnFireBullet;
        }
    }
}