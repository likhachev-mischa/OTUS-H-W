using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathController : MonoBehaviour,
        IGameStartListener,
        IGameFinishListener
    {
        private GameManager gameManager;

        [SerializeField] private GameObject character;

        private DeathComponent deathComponent;

        private void Awake()
        {
            this.deathComponent = this.character.GetComponent<DeathComponent>();
            this.gameManager = FindObjectOfType<GameManager>();
        }

        public void OnStart()
        {
            deathComponent.Enable();
            this.deathComponent.DeathEvent += this.OnCharacterDeath;
        }

        public void OnFinish()
        {
            deathComponent.Disable();
            this.deathComponent.DeathEvent -= this.OnCharacterDeath;
        }

        private void OnCharacterDeath()
        {
            this.gameManager.FinishGame();
        }
    }
}