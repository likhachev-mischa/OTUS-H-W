using UnityEngine;

namespace ShootEmUp
{
    public static class GameManager
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