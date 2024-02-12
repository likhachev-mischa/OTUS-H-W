using System;
using Sample;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Equipment
{
    public sealed class EquipmentPresenter : MonoBehaviour
    {
        private Inventory inventory;
        private Equipment equipment;

        public void Construct(Inventory inventory, Equipment equipment)
        {
            this.inventory = inventory;
            this.equipment = equipment;
        }

        [Button]
        public void EquipItem(string name)
        {
            Item item = ValidateItem(name);

            EquipmentType type = item.GetComponent<EquipmentComponent>().Type;
            equipment.AddItem(type, item);
        }

        [Button]
        public void UnEquipItem(string name)
        {
            Item item = ValidateItem(name);

            EquipmentType type = item.GetComponent<EquipmentComponent>().Type;
            equipment.RemoveItem(type);
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