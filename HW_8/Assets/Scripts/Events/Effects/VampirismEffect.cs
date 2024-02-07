using System;
using Entities;
using Entities.Components;
using Events.Effects;
using UnityEngine;

namespace Handlers.Turn
{
    [Serializable]
    public struct VampirismEffect : IEffect
    {
        public IEntity Source { get; set; }
        public Target Target { get; set; }
        public IEffect NextEffect { get; set; }

        [field:SerializeField]
        public float Probability { get; set; }

        [SerializeReference]
        public IEffect[] SuccessEffects;
    }
}