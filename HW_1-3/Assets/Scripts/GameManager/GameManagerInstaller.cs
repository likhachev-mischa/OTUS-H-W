using UnityEngine;

namespace ShootEmUp
{
    public class GameManagerInstaller : GameInstaller
    {
        [Service(typeof(GameManager))] [SerializeField]
        private GameManager gameManager;

        [Service(typeof(InputManager))] [SerializeField] [Listener]
        private InputManager inputManager;
    }
}