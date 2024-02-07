using System;
using Entities;
using Entities.Components;
using Events.Effects;

namespace Events.Requests
{
    [Serializable]
    public struct AttackFinishedRequest : IEffect
    {
        public IEntity Source { get; set; }
        public Target Target { get; set; }
        public IEffect NextEffect { get; set; }
    }
}