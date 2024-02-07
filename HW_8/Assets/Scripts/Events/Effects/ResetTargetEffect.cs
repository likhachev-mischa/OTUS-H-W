using System;
using Entities;
using Entities.Components;
using UnityEngine;

namespace Events.Effects
{
    [Serializable]
    public struct ResetTargetEffect : IEffect
    {
        public IEntity Source { get; set; }
        public Target Target { get; set; }
        
        [field:SerializeField]
        public float Probability { get; private set; }
    }
}