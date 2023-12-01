using UnityEngine;

namespace ShootEmUp
{
    public class MenuInstaller : GameInstaller
    {
        [Listener] [SerializeField] private MenuManager menuManager;
    }
}