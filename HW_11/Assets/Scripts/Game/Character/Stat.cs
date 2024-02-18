using System;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public struct Stat
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Value { get; private set; }

        public Stat(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}