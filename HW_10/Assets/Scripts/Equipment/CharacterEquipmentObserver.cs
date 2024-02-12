using System;
using Sample;

namespace Equipment
{
    public sealed class CharacterEquipmentObserver : IDisposable
    {
        private Character character;
        private Equipment equipment;

        public CharacterEquipmentObserver(Character character, Equipment equipment)
        {
            this.character = character;
            this.equipment = equipment;

            this.equipment.OnItemAdded += OnItemAdded;
            this.equipment.OnItemRemoved += OnItemRemoved;
            this.equipment.OnItemChanged += OnItemChanged;
        }

        private void OnItemChanged(Item prevItem, Item newItem)
        {
            OnItemRemoved(prevItem);
            OnItemAdded(newItem);
        }

        private void OnItemRemoved(Item obj)
        {
            Stat[] stats = obj.GetComponent<EquipmentComponent>().Stats;

            for (var i = 0; i < stats.Length; i++)
            {
                int prevValue = character.GetStat(stats[i].Name);
                int newValue = prevValue - stats[i].Value;

                character.SetStat(stats[i].Name, newValue);
            }
        }
        
        private void OnItemAdded(Item obj)
        {
            Stat[] stats = obj.GetComponent<EquipmentComponent>().Stats;

            for (var i = 0; i < stats.Length; i++)
            {
                int prevValue = character.GetStat(stats[i].Name);
                int newValue = prevValue + stats[i].Value;

                character.SetStat(stats[i].Name, newValue);
            }
        }

        public void Dispose()
        {
            this.equipment.OnItemAdded -= OnItemAdded;
            this.equipment.OnItemRemoved -= OnItemRemoved;
            this.equipment.OnItemChanged -= OnItemChanged;
        }
    }
}