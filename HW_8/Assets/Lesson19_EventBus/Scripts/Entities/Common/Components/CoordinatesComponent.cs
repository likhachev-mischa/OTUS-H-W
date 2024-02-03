using Lessons.Utils;
using UnityEngine;

namespace Lessons.Entities.Common.Components
{
    public sealed class CoordinatesComponent
    {
        public Vector2Int Value
        {
            get => _coordinates;
            set => _coordinates.Value = value;
        }
        
        private readonly AtomicVariable<Vector2Int> _coordinates;

        public CoordinatesComponent(AtomicVariable<Vector2Int> coordinates)
        {
            _coordinates = coordinates;
        }
    }
}