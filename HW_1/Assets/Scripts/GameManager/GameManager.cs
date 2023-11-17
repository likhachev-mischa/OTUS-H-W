using UnityEngine;

namespace ShootEmUp
{
    public static class GameManager
    {
        public static void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}