using UnityEngine;

namespace ShootEmUp
{
    public sealed class TeamComponent : MonoBehaviour
    {
        [SerializeField] private bool isPlayer;
        public bool IsPlayer => isPlayer;
    }
}