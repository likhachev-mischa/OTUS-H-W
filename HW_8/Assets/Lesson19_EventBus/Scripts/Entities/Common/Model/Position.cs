using System;
using Lessons.Utils;
using UnityEngine;

namespace Lessons.Entities.Common.Model
{
    [Serializable]
    public sealed class Position
    {
        public Transform transform;
        
        public AtomicVariable<Vector2Int> coordinates;   
    }
}