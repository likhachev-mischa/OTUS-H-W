using DI;
using UnityEngine;

namespace Game.UI
{
    public class GameFinishView : MonoBehaviour,IGameFinishListener
    {
        [SerializeField] private GameObject gameOverView;
        
        public void OnFinish()
        {
            gameOverView.SetActive(true);
        }
    }
}