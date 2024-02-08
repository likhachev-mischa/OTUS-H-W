using System;
using Entities;
using Entities.Components;
using Events.Effects;

namespace Events.Requests
{
    [Serializable]
    public struct TurnFinishedRequest : IEffect
    {
        public IEntity Source { get; set; }
        public Target Target { get; set; }
    }
}