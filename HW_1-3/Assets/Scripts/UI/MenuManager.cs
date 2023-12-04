using System.Collections;
using TMPro;
using UnityEngine;

namespace ShootEmUp
{
    public class MenuManager : MonoBehaviour,
        IGameFinishListener,
        IGameUpdateListener
    {
        [SerializeField] private TextMeshProUGUI countdownText;
        [SerializeField] private GameObject startButton;
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject inGameMenu;
        [SerializeField] private GameObject finishMenu;
        [SerializeField] private TextMeshProUGUI characterHealthDisplay;
        [SerializeField] private int startDelay;

        private HealthComponent characterHealth;
        private GameManager gameManager;

        [Inject]
        private void Construct(GameManager gameManager, Character character)
        {
            this.gameManager = gameManager;
            characterHealth = character.GetComponent<HealthComponent>();
        }

        public void OnStartButtonClick()
        {
            StartCoroutine(StartGame(startDelay));
        }

        private IEnumerator StartGame(int delay)
        {
            startButton.SetActive(false);
            countdownText.gameObject.SetActive(true);
            for (int i = delay; i > 0; --i)
            {
                countdownText.text = $"{i}";
                yield return new WaitForSeconds(1);
            }

            startMenu.SetActive(false);
            inGameMenu.SetActive(true);
            gameManager.StartGame();
        }

        public void OnPauseButtonClick()
        {
            if (gameManager.State == GameState.ON)
            {
                gameManager.PauseGame();
                pauseMenu.SetActive(true);
            }
            else
            {
                gameManager.ResumeGame();
                pauseMenu.SetActive(false);
            }
        }

        public void OnFinish()
        {
            inGameMenu.SetActive(false);
            finishMenu.SetActive(true);
        }

        public void OnUpdate(float deltaTime)
        {
            characterHealthDisplay.text = $" HP {characterHealth.Health}";
        }
    }
}