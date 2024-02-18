using System;

namespace Sample
{
    public sealed class EquipmentInventoryObserver : IDisposable
    {
        private Equipment equipment;
        private Inventory inventory;

        public EquipmentInventoryObserver(Equipment equipment, Inventory inventory)
        {
            this.equipment = equipment;
            this.inventory = inventory;

            this.inventory.OnItemRemoved += OnItemRemoved;
        }
        
        private void OnItemRemoved(Item item)
        {
            if (item.Flags.HasFlag(ItemFlags.EQUPPABLE))
            {
                EquipmentType type = item.GetComponent<EquipmentComponent>().Type;
                if (equipment.HasItem(type))
                {
                    equipment.RemoveItem(type);
                }
            }
        }

        public void Dispose()
        {
            inventory.OnItemRemoved -= OnItemRemoved;
        }
        
    }
}