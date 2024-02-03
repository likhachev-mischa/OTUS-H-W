using DG.Tweening;
using Entities;
using Lessons.Entities.Common.Components;
using UnityEngine;

namespace Lessons.Game.Pipeline.Visual.Tasks
{
    public sealed class MoveVisualTask : Task
    {
        private readonly TransformComponent _transform;
        private readonly Vector3 _position;
        private readonly float _duration;

        public MoveVisualTask(IEntity entity, Vector3 position, float duration = 0.15f)
        {
            _transform = entity.Get<TransformComponent>();
            _position = position;
            _duration = duration;
        }
        
        protected override void OnRun()
        {
            _transform.Value.DOMove(_position, _duration).OnComplete(Finish);
        }
    }
}