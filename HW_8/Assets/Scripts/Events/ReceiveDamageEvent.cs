using System;
using Entities;
using Entities.Components;
using Game.EventBus;
using UnityEngine;

namespace Events
{
    [Serializable]
    public struct ReceiveDamageEvent : IEvent
    {
        public readonly IEntity Source;
        public readonly Target Target;
        public readonly int Damage;
        public ReceiveDamageEvent(IEntity source, Target target, int damage)
        {
            Source = source;
            Target = target;
            Damage = damage;
        }
    }
}