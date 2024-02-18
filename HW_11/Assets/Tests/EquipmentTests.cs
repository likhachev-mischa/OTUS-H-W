using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sample;

[TestFixture]
public sealed class EquipmentTest
{
    private Character character;
    private Stat[] characterStats;

    private Inventory inventory;
    private Equipment equipment;

    private EquipmentInventoryAdapter equipmentInventoryAdapter;
    private CharacterEquipmentObserver characterEquipmentObserver;
    private EquipmentInventoryObserver equipmentInventoryObserver;

    private Item boots;
    private Item arcaneBoots;

    [SetUp]
    public void Setup()
    {
        var moveSpeed = new Stat("MoveSpeed", 15);

        var intelligence = new Stat("Intelligence", 5);

        characterStats = new[] { moveSpeed, intelligence };

        var keyValue = new KeyValuePair<string, int>[characterStats.Length];
        for (var i = 0; i < characterStats.Length; i++)
        {
            keyValue[i] = new KeyValuePair<string, int>(characterStats[i].Name, characterStats[i].Value);
        }

        character = new Character(keyValue);

        boots = new Item("Boots", ItemFlags.EQUPPABLE, new EquipmentComponent(EquipmentType.LEGS, moveSpeed));

        var arcaneMoveSpeed = new Stat("MoveSpeed", 10);
        arcaneBoots = new Item("ArcaneBoots", ItemFlags.EQUPPABLE,
            new EquipmentComponent(EquipmentType.LEGS, arcaneMoveSpeed, intelligence));

        inventory = new Inventory(boots, arcaneBoots);
        equipment = new Equipment();

        equipmentInventoryAdapter = new EquipmentInventoryAdapter(inventory, equipment);
        characterEquipmentObserver = new CharacterEquipmentObserver(character, equipment);
        equipmentInventoryObserver = new EquipmentInventoryObserver(equipment, inventory);
    }

    [Test]
    public void CharacterStatsChangeOnItemEquippedTest()
    {
        string statName = "MoveSpeed";

        int initialStatValue = character.GetStat(statName);
        equipmentInventoryAdapter.EquipItem(boots.Name);
        int equippedStatValue = character.GetStat(statName);

        Assert.False(initialStatValue == equippedStatValue);

        equipmentInventoryAdapter.UnEquipItem(boots.Name);
        int unequippedStatValue = character.GetStat(statName);

        Assert.True(initialStatValue == unequippedStatValue);
    }

    [Test]
    public void CharacterStatsChangeOnEquipmentChangeTest()
    {
        string statName = "MoveSpeed";
        equipmentInventoryAdapter.EquipItem(boots.Name);
        int equippedStatValue = character.GetStat(statName);
        equipmentInventoryAdapter.ChangeItem(arcaneBoots.Name);
        int changedStatValue = character.GetStat(statName);

        Assert.False(equippedStatValue == changedStatValue);
    }

    [Test]
    public void ExceptionOnEquippingItemToOccupiedSlotTest()
    {
        equipmentInventoryAdapter.EquipItem(boots.Name);
        Assert.Throws<Exception>(() => equipmentInventoryAdapter.EquipItem(arcaneBoots.Name));
    }

    [Test]
    public void ItemUnEquipOnRemovalFromInventoryTest()
    {
        EquipmentType type = boots.GetComponent<EquipmentComponent>().Type;

        equipmentInventoryAdapter.EquipItem(boots.Name);
        bool hadItem = equipment.HasItem(type);
        inventory.RemoveItem(boots);
        bool hasItem = equipment.HasItem(type);

        Assert.True(hadItem);
        Assert.False(hasItem);
    }
}