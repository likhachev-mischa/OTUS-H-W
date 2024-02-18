using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    public sealed class EquipmentPresenter : MonoBehaviour
    {
        private EquipmentInventoryAdapter equipmentInventoryAdapter;

        public void Construct(Inventory inventory, Equipment equipment)
        {
            equipmentInventoryAdapter = new EquipmentInventoryAdapter(inventory, equipment);
        }

        [Button]
        public void EquipItem(string name)
        {
            equipmentInventoryAdapter.EquipItem(name);
        }

        [Button]
        public void UnEquipItem(string name)
        {
            equipmentInventoryAdapter.UnEquipItem(name);
        }
    }
}