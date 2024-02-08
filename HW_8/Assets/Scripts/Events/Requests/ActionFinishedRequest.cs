using System;
using Entities;
using Entities.Components;

namespace Events.Effects
{
    [Serializable]
    public struct ActionFinishedRequest : IEffect
    {
        public IEntity Source { get; set; }
        public Target Target { get; set; }
    }
}