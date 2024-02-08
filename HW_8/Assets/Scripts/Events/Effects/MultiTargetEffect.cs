using Entities;
using Entities.Components;
using UnityEngine;

namespace Events.Effects
{
    public class MultiTargetEffect : IEffect
    {
        public IEntity Source { get; set; }
        public Target Target { get; set; }

        [SerializeReference]
        public IEffect[] Effects;
    }
}