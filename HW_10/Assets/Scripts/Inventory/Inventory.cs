using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Sample
{
    //Нельзя менять!
    [Serializable]
    public sealed class Inventory
    {
        public event Action<Item> OnItemAdded;
        public event Action<Item> OnItemRemoved; 

        [ShowInInspector, ReadOnly]
        private List<Item> items;

        public Inventory(params Item[] items)
        {
            this.items = new List<Item>(items);
        }

        public void Setup(params Item[] items)
        {
            this.items = new List<Item>(items);
        }
        
        public void AddItem(Item item)
        {
            if (!this.items.Contains(item))
            {
                this.items.Add(item);
                this.OnItemAdded?.Invoke(item);
            }
        }
        
        public void RemoveItem(Item item)
        {
            if (this.items.Remove(item))
            {
                this.OnItemRemoved?.Invoke(item);
            }
        }

        public void RemoveItems(string name, int count)
        {
            for (int i = 0; i < count; i++)
            {
                this.RemoveItem(name);
            }
        }

        public void RemoveItem(string name)
        {
            if (this.FindItem(name, out var item))
            {
                this.RemoveItem(item);
            }
        }

        public List<Item> GetItems()
        {
            return this.items.ToList();
        }

        public bool FindItem(string name, out Item result)
        {
            foreach (var inventoryItem in this.items)
            {
                if (inventoryItem.Name == name)
                {
                    result = inventoryItem;
                    return true;
                }
            }
            
            result = null;
            return false;
        }

        public int GetCount(string item)
        {
            return this.items.Count(it => it.Name == item);
        }
    }
}