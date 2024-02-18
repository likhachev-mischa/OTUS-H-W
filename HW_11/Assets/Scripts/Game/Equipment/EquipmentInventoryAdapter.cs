using System;

namespace Sample
{
    public sealed class EquipmentInventoryAdapter
    {
        private Inventory inventory;
        private Equipment equipment;

        public EquipmentInventoryAdapter(Inventory inventory, Equipment equipment)
        {
            this.inventory = inventory;
            this.equipment = equipment;
        }
        
        public void EquipItem(string name)
        {
            Item item = ValidateItem(name);

            EquipmentType type = item.GetComponent<EquipmentComponent>().Type;
            equipment.AddItem(type, item);
        }
        
        public void UnEquipItem(string name)
        {
            Item item = ValidateItem(name);

            EquipmentType type = item.GetComponent<EquipmentComponent>().Type;
            equipment.RemoveItem(type);
        }

        public void ChangeItem(string name)
        {
            Item item = ValidateItem(name);

            EquipmentType type = item.GetComponent<EquipmentComponent>().Type;
            equipment.ChangeItem(type,item);
        }

        private Item ValidateItem(string name)
        {
            if (!inventory.FindItem(name, out Item item))
            {
                throw new Exception($"Item {name} was not present in inventory!");
            }

            if (!item.Flags.HasFlag(ItemFlags.EQUPPABLE))
            {
                throw new Exception($"Item {name} can't be equipped!");
            }

            return item;
        }
    }
}