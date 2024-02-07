using System;
using Entities;
using Entities.Components;
using UnityEngine;

namespace Events.Effects
{
    [Serializable]
    public struct DealDamageEffect : IEffect
    { 
        public IEntity Source { get; set; }
        public Target Target { get; set; }
    }
}