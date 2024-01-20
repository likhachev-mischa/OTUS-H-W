using System;
using DI;
using UnityEngine;

namespace Game.Installers
{
    [Serializable]
    public sealed class ControllersInstaller : GameInstaller
    {
        [Service(typeof(Camera))] [SerializeField]
        private Camera camera;

        [Listener] private MovementController movementController = new();
        [Listener] private RotationController rotationController = new();
    }
}