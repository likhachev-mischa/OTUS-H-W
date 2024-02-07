using System;
using Entities;
using Entities.Components;
using Game.EventBus;
using UnityEngine;

namespace Events
{
    [Serializable]
    public struct SwitchToRandomTargetEvent: IEvent
    {
        public IEntity Source;
        public Target Target;
        public float Probability;

        public SwitchToRandomTargetEvent(IEntity source, Target target, float probability)
        {
            Source = source;
            Target = target;
            Probability = probability;
        }
    }
}