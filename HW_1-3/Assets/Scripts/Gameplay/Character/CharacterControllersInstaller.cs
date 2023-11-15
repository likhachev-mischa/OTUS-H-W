using UnityEngine;

namespace ShootEmUp
{
    public class CharacterControllersInstaller : GameInstaller
    {
        [Listener] private CharacterDeathController deathController = new();
        [Listener] private CharacterShootController shootController = new();
        [Listener] private CharacterMovementController movementController = new();

        [Service(typeof(ContainInBoundsCorrector))] [SerializeField]
        private ContainInBoundsCorrector containInBoundsCorrector = new();
    }
}