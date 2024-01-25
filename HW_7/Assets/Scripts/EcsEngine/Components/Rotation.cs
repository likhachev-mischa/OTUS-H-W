using System;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct Rotation
    {
        public Quaternion value;
    }
}