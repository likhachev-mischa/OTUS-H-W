﻿using System.Collections.Generic;
using Sample;
using UnityEngine;

namespace Equipment
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
        private EquipmentInventoryAdapter equipmentInventoryAdapter;


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
            equipmentInventoryAdapter = new EquipmentInventoryAdapter(equipment, inventory);
        }

        private void OnEnable()
        {
            equipmentPresenter.Construct(inventory, equipment);
            inventoryPresenter.Construct(inventory);
        }

        private void OnDestroy()
        {
            characterEquipmentObserver.Dispose();
            equipmentInventoryAdapter.Dispose();
        }
    }
}