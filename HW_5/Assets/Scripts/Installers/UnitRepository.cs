using System;
using DI;
using GameEngine;
using UnityEngine;

namespace Installers
{
    [Serializable]
    public class UnitRepository : GameInstaller
    {
        [Service(typeof(Unit[]))]
        [SerializeField] private Unit[] unitPrefabs;
        
    }
}