using Entities;
using JetBrains.Annotations;
using Lessons.Game.Events;
using Lessons.Game.Services;
using UnityEngine;

namespace Lessons.Game.Pipeline.Turn.Tasks
{
    [UsedImplicitly]
    public sealed class PlayerTurnTask : Task
    {
        private readonly KeyboardInput _input;
        private readonly EventBus _eventBus;

        private readonly IEntity _player;
        
        public PlayerTurnTask(KeyboardInput input, EventBus eventBus, PlayerService playerService)
        {
            _input = input;
            _eventBus = eventBus;
            _player = playerService.Player;
        }

        protected override void OnRun()
        {
            _input.OnMove += OnMovePerformed;
        }

        protected override void OnFinish()
        {
            _input.OnMove -= OnMovePerformed;
        }

        private void OnMovePerformed(Vector2Int direction)
        {
            _eventBus.RaiseEvent(new ApplyDirectionEvent(_player, direction));
            
            Finish();
        }
    }
}