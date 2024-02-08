using System;
using Entities;
using Entities.Components;
using UnityEngine;

namespace Events.Effects
{
    [Serializable]
    public struct DamageBlockEffect : IEffect
    {
        public IEntity Source { get; set; }
        public Target Target { get; set; }

        public bool IsInfinite;

        public int Amount;

        [SerializeReference] public IEffect[] SuccessEffects;
    }
}