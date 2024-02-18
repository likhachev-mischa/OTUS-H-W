using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    public sealed class EquipmentTest : MonoBehaviour
    {
        [SerializeField] private Equipment equipment = new();
        [SerializeField] private EquipmentPresenter equipmentPresenter;
        [SerializeField] private Inventory inventory = new();
        [SerializeField] private InventoryPresenter inventoryPresenter;
        [SerializeField] private CharacterStatsConfig characterStats;
        [SerializeField] private Character character;

        private CharacterEquipmentObserver characterEquipmentObserver;
        private EquipmentInventoryObserver equipmentInventoryObserver;


        private void Awake()
        {
            Stat[] stats = characterStats.stats;
            var keyValue = new KeyValuePair<string, int>[stats.Length];

            for (var i = 0; i < stats.Length; i++)
            {
                keyValue[i] = new KeyValuePair<string, int>(stats[i].Name, stats[i].Value);
            }

            character = new Character(keyValue);
            characterEquipmentObserver = new CharacterEquipmentObserver(character, equipment);
            equipmentInventoryObserver = new EquipmentInventoryObserver(equipment, inventory);
        }

        private void OnEnable()
        {
            equipmentPresenter.Construct(inventory, equipment);
            inventoryPresenter.Construct(inventory);
        }

        private void OnDestroy()
        {
            characterEquipmentObserver.Dispose();
            equipmentInventoryObserver.Dispose();
        }
    }
}