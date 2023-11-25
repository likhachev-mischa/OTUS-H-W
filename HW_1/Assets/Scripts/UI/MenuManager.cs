using System.Collections;
using TMPro;
using UnityEngine;

namespace ShootEmUp
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        [SerializeField] private GameObject menu;
        [SerializeField] private TextMeshProUGUI countdownText;
        [SerializeField] private GameObject button;

        [SerializeField] private int startDelay;

        public void OnStartButtonClick()
        {
            StartCoroutine(StartGame(startDelay));
        }

        IEnumerator StartGame(int delay)
        {
            this.button.SetActive(false);
            for (int i = delay; i > 0; --i)
            {
                this.countdownText.text = $"{i}";
                yield return new WaitForSeconds(1);
            }
            this.menu.SetActive(false);
            this.gameManager.StartGame();
        }
    }
}