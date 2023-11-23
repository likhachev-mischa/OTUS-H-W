using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathController : MonoBehaviour,
        IGameStartListener,
        IGameFinishListener
    {
        private GameFinisher gameFinisher;

        [SerializeField] private GameObject character;

        private DeathComponent deathComponent;

        private void Awake()
        {
            this.deathComponent = this.character.GetComponent<DeathComponent>();
            this.gameFinisher = FindObjectOfType<GameFinisher>();
        }

        public void OnStart()
        {
            this.deathComponent.DeathEvent += this.OnCharacterDeath;
        }

        public void OnFinish()
        {
            this.deathComponent.DeathEvent -= this.OnCharacterDeath;
        }

        private void OnCharacterDeath()
        {
            this.gameFinisher.FinishGame();
        }
    }
}