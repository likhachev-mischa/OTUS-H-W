using UnityEngine;

namespace Lessons.Entities.Common.Components
{
    public sealed class PositionComponent
    {
        public Vector3 Value
        {
            get => _transform.position;
            set => _transform.position = value;
        }
        
        private readonly Transform _transform;
        
        public PositionComponent(Transform transform)
        {
            _transform = transform;
        }
    }
}