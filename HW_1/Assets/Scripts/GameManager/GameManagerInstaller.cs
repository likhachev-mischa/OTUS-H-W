using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootEmUp
{
    public class GameManagerInstaller : MonoBehaviour
    {
        private void Awake()
        {
            var manager = GetComponent<GameManager>();
            var scene = SceneManager.GetActiveScene();
            var gameObjects = scene.GetRootGameObjects();

            for (int i = 0, count1 = gameObjects.Length; i < count1; i++)
            {
                var listeners = gameObjects[i].GetComponentsInChildren<IGameListener>();
                for (int j = 0, count2 = listeners.Length; j < count2; j++)
                {
                    manager.AddListener(listeners[j]);
                }
            }
            
        }
    }
}