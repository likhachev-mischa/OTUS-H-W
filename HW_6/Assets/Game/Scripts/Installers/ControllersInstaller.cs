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

        [Service(typeof(KillCounter))] [Listener]
        private KillCounter killCounter = new();

        [Listener] private GameFinisherController gameFinisherController = new();

        [Listener] private MovementController movementController = new();
        [Listener] private RotationController rotationController = new();
        [Listener] private ShootController shootController = new();
        [Listener] [SerializeField] private CameraController cameraController = new();
    }
}