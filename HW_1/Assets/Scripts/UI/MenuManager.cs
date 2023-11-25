using System.Collections;
using TMPro;
using UnityEngine;

namespace ShootEmUp
{
    public class MenuManager : MonoBehaviour,
        IGameFinishListener,
        IGameUpdateListener
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private TextMeshProUGUI countdownText;
        [SerializeField] private GameObject startButton;
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject inGameMenu;
        [SerializeField] private GameObject finishMenu;

        [SerializeField] private TextMeshProUGUI characterHealthDisplay;

        [SerializeField] private HealthComponent characterHealth;


        [SerializeField] private int startDelay;

        public void OnStartButtonClick()
        {
            StartCoroutine(StartGame(startDelay));
        }

        IEnumerator StartGame(int delay)
        {
            this.startButton.SetActive(false);
            for (int i = delay; i > 0; --i)
            {
                this.countdownText.text = $"{i}";
                yield return new WaitForSeconds(1);
            }

            this.startMenu.SetActive(false);
            this.inGameMenu.SetActive(true);
            this.gameManager.StartGame();
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
            this.inGameMenu.SetActive(false);
            this.finishMenu.SetActive(true);
        }

        public void OnUpdate(float deltaTime)
        {
            characterHealthDisplay.text = $" HP {characterHealth.Health}";
        }
    }
}