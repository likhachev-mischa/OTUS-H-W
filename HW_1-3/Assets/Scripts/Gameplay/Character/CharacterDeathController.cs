using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterDeathController :
        IGameStartListener,
        IGameFinishListener
    {
        private GameManager gameManager;
        private DeathComponent deathComponent;

        [Inject]
        public void Construct(GameManager gameManager,
            Character character)
        {
            this.gameManager = gameManager;
            this.deathComponent = character.GetComponent<DeathComponent>();
        }

        public void OnStart()
        {
            this.deathComponent.Enable();
            this.deathComponent.DeathEvent += this.OnCharacterDeath;
        }

        public void OnFinish()
        {
            this.deathComponent.Disable();
            this.deathComponent.DeathEvent -= this.OnCharacterDeath;
        }

        private void OnCharacterDeath()
        {
            this.gameManager.FinishGame();
        }
    }
}