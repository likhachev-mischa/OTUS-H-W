using System;
using Sample;
using UnityEngine;

namespace Equipment
{
    [Serializable]
    public struct EquipmentComponent
    {
        [field: SerializeField] public EquipmentType Type { get; private set; }

        [field: SerializeReference] public Stat[] Stats { get; private set; }
    }
}