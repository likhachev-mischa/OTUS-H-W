using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    public sealed class InventoryPresenter : MonoBehaviour
    {
        private Inventory inventory;

        public void Construct(Inventory inventory)
        {
            this.inventory = inventory;
        }

        [Button]
        public void AddItem(ItemConfig itemConfig)
        {
            Item item = itemConfig.item.Clone();
            inventory.AddItem(item);
        }

        [Button]
        public void RemoveItem(ItemConfig itemConfig)
        {
            string id = itemConfig.item.Name;
            inventory.RemoveItem(id);
        }
    }
}