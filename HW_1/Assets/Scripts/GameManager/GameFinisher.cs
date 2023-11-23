using UnityEngine;

namespace ShootEmUp
{
    public class GameFinisher : MonoBehaviour
    {
        public bool IsGameOver { get; private set; }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            IsGameOver = true;
            Time.timeScale = 0;
        }
    }
}