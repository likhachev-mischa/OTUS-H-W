using UnityEngine;

namespace ShootEmUp
{
    namespace Components
    {
         public sealed class TeamComponent : MonoBehaviour
        {
            [SerializeField] private bool isPlayer;
            public bool IsPlayer => this.isPlayer;
        }
    }
}