using System;
using System.Collections.Generic;
using System.Linq;
using Sample;

namespace Equipment
{
    //TODO: Реализовать экипировку
    public sealed class Equipment
    {
        public event Action<Item> OnItemAdded;
        public event Action<Item> OnItemRemoved;
        public event Action<Item> OnItemChanged;

        private Dictionary<EquipmentType, Item> dictionary;

        public void Setup(IEnumerable<KeyValuePair<EquipmentType, Item>> items)
        {
            dictionary = new Dictionary<EquipmentType, Item>(items);
        }

        public Item GetItem(EquipmentType type)
        {
            if (!dictionary.ContainsKey(type))
            {
                throw new Exception($"Equipment has no items of type {type}.");
            }

            return dictionary[type];
        }

        public bool TryGetItem(EquipmentType type, out Item result)
        {
            return dictionary.TryGetValue(type, out result);
        }

        public void RemoveItem(EquipmentType type)
        {
            Item item = dictionary[type];
            dictionary.Remove(type);
            OnItemRemoved?.Invoke(item);
        }

        public void AddItem(EquipmentType type, Item item)
        {
            if (dictionary.ContainsKey(type))
            {
                throw new Exception($"Equipment already has item of type {type}. Remove it before adding new.");
            }

            dictionary[type] = item;
            OnItemAdded?.Invoke(item);
        }

        public void ChangeItem(EquipmentType type, Item item)
        {
            bool contains = dictionary.ContainsKey(type);
            dictionary[type] = item;
            if (!contains)
            {
                OnItemAdded?.Invoke(item);
            }
            else
            {
                OnItemChanged?.Invoke(item);
            }
        }

        public bool HasItem(EquipmentType type)
        {
            return dictionary.ContainsKey(type);
        }

        public KeyValuePair<EquipmentType, Item>[] GetItems()
        {
            return dictionary.ToArray();
        }
    }
}