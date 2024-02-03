using System;
using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Game.Events.Effects;
using UnityEngine;
using VContainer.Unity;

namespace Lessons.Game.Handlers.Effects
{
    [UsedImplicitly]
    public sealed class PushEffectHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;

        public PushEffectHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        void IInitializable.Initialize()
        {
            _eventBus.Subscribe<PushEffectEvent>(OnPush);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<PushEffectEvent>(OnPush);
        }

        private void OnPush(PushEffectEvent evt)
        {
            CoordinatesComponent coordinates = evt.Source.Get<CoordinatesComponent>();
            CoordinatesComponent targetCoordinates = evt.Target.Get<CoordinatesComponent>();

            Vector2Int direction = targetCoordinates.Value - coordinates.Value;
            
            _eventBus.RaiseEvent(new ForceDirectionEvent(evt.Target, direction));
        }
    }
}