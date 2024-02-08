using System;
using Entities;
using Entities.Components;
using UnityEngine;

namespace Events.Effects
{
    [Serializable]
    public struct SwitchToRandomTargetEffect : IEffect
    {
        public IEntity Source { get; set; }
        public Target Target { get; set; }

        public bool IsAimedOnAllies;
        [field: SerializeField] public float Probability { get; set; }
        
        [SerializeReference]
        public IEffect[] SuccessEffects;
    }
}