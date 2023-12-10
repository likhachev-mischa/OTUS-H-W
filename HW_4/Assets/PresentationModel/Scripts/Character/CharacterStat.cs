using System;
using UnityEngine;

namespace MVVM
{
    [Serializable]
    public sealed class CharacterStat
    {
        public event Action<CharacterStat,int> OnValueChanged;

        public string Name
        {
            get => name;
            private set => name = value;
        }

        public int Value
        {
            get => this.value;
            private set => this.value = value;
        }

        [SerializeField] [ReadOnly] private string name;
        [SerializeField] [ReadOnly] private int value;

        public CharacterStat(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }

        public void ChangeValue(int value)
        {
            this.Value = value;
            this.OnValueChanged?.Invoke(this,value);
        }
    }
}