using System;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public struct EquipmentComponent
    {
        [field: SerializeField] public EquipmentType Type { get; private set; }
        [field: SerializeField] public Stat[] Stats { get; private set; }

        public EquipmentComponent(EquipmentType type, params Stat[] stats)
        {
            this.Type = type;
            this.Stats = new Stat[stats.Length];
            
            for (var i = 0; i < this.Stats.Length; i++)
            {
                this.Stats[i] = stats[i];
            }
        }
    }
}