using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathController : MonoBehaviour
    {
        private GameManager gameManager;

        [SerializeField] private GameObject character;

        private DeathComponent deathComponent;

        private void Awake()
        {
            this.deathComponent = this.character.GetComponent<DeathComponent>();
            this.gameManager = FindObjectOfType<GameManager>();
        }

        private void OnEnable()
        {
            this.deathComponent.DeathEvent += this.OnCharacterDeath;
        }

        private void OnDisable()
        {
            this.deathComponent.DeathEvent -= this.OnCharacterDeath;
        }


        private void OnCharacterDeath()
        {
            this.gameManager.FinishGame();
        }
    }
}