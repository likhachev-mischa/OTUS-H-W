using System;
using System.Collections.Generic;
using System.Linq;
using Sample;
using Sirenix.OdinInspector;

namespace Equipment
{
    [Serializable]
    public sealed class Equipment
    {
        public event Action<Item> OnItemAdded;
        public event Action<Item> OnItemRemoved;
        public event Action<Item, Item> OnItemChanged;

        [ShowInInspector] [ReadOnly] private Dictionary<EquipmentType, Item> items;

        public Equipment()
        {
            items = new Dictionary<EquipmentType, Item>();
        }

        public Equipment(IEnumerable<KeyValuePair<EquipmentType, Item>> items)
        {
            this.items = new Dictionary<EquipmentType, Item>(items);
        }

        public Item GetItem(EquipmentType type)
        {
            if (!items.ContainsKey(type))
            {
                throw new Exception($"Equipment has no items of type {type}.");
            }

            return items[type];
        }

        public bool TryGetItem(EquipmentType type, out Item result)
        {
            return items.TryGetValue(type, out result);
        }

        public void RemoveItem(EquipmentType type)
        {
            Item item = items[type];
            items.Remove(type);
            OnItemRemoved?.Invoke(item);
        }

        public void AddItem(EquipmentType type, Item item)
        {
            if (items.ContainsKey(type))
            {
                throw new Exception($"Equipment already has item of type {type}. Remove it before adding new.");
            }

            items[type] = item;
            OnItemAdded?.Invoke(item);
        }

        public void ChangeItem(EquipmentType type, Item item)
        {
            bool contains = items.ContainsKey(type);
            Item prevItem = items[type];
            items[type] = item;
            if (!contains)
            {
                OnItemAdded?.Invoke(item);
            }
            else
            {
                OnItemChanged?.Invoke(prevItem, item);
            }
        }

        public bool HasItem(EquipmentType type)
        {
            return items.ContainsKey(type);
        }

        public KeyValuePair<EquipmentType, Item>[] GetItems()
        {
            return items.ToArray();
        }
    }
}