using System;
using Sample;

namespace Equipment
{
    public sealed class EquipmentInventoryAdapter : IDisposable
    {
        private Equipment equipment;
        private Inventory inventory;

        public EquipmentInventoryAdapter(Equipment equipment, Inventory inventory)
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