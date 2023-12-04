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
            deathComponent = character.GetComponent<DeathComponent>();
        }

        public void OnStart()
        {
            deathComponent.Enable();
            deathComponent.DeathEvent += OnCharacterDeath;
        }

        public void OnFinish()
        {
            deathComponent.Disable();
            deathComponent.DeathEvent -= OnCharacterDeath;
        }

        private void OnCharacterDeath()
        {
            gameManager.FinishGame();
        }
    }
}