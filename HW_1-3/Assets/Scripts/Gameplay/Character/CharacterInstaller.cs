using System.Runtime.CompilerServices;
using UnityEngine;

namespace ShootEmUp
{
    public class CharacterInstaller : GameInstaller
    {
        [Service(typeof(Character))] [SerializeField]
        private Character character;
    }
}