using UnityEngine;

namespace ShootEmUp
{
    public class GameManagerInstaller : MonoBehaviour
    {
        private void Awake()
        {
            var manager = GetComponent<GameManager>();
            var listeners = GetComponentsInChildren<IGameListener>();

            for (int i = 0, count = listeners.Length; i < count; i++)
            {
                manager.AddListener(listeners[i]);
            }
        }
    }
}