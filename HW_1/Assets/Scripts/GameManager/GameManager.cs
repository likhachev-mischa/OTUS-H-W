using UnityEngine;

namespace ShootEmUp
{
    public class GameManager :MonoBehaviour
    {
        public static bool IsGameOver { get; private set; } = false;

        public static void FinishGame()
        {
            Debug.Log("Game over!");
            IsGameOver = true;
            Time.timeScale = 0;
        }
    }
}