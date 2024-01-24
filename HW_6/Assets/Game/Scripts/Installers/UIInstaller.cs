using System;
using DI;
using Game.UI;
using UnityEngine;

namespace Game.Installers
{
    [Serializable]
    public class UIInstaller : GameInstaller
    {
        [SerializeField] [Listener] private GameFinishView gameFinishView;
    }
}