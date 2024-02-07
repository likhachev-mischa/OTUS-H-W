using System;
using Entities;
using Entities.Components;
using Events.Effects;
using Game.EventBus;
using UnityEngine;

namespace Handlers.Turn
{
    [Serializable]
    public struct VampirismEvent : IEvent
    {
        public IEntity Source;
        public Target Target;
        public float Probability;

        public VampirismEvent(IEntity source, Target target, float probability)
        {
            Source = source;
            Target = target;
            Probability = probability;
        }
    }
}