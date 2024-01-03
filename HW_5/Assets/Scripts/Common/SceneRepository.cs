using System;
using System.Collections.Generic;
using DI;
using GameEngine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common
{
    [Serializable]
    public class SceneRepository : GameInstaller
    {
        [Service(typeof(Unit[]))]
        [SerializeField] private Unit[] unitPrefabs;

        /*[Service(typeof(Resource[]))]
        private Resource[] resources = Object.FindObjectsOfType<Resource>();*/
    }
}