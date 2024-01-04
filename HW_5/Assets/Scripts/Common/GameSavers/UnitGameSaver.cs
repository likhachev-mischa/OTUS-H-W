using System.Collections.Generic;
using System.Linq;
using DI;
using GameEngine;
using SaveSystem;
using UnityEngine;

namespace GameSavers
{
    public struct UnitData
    {
        public string Type;
        public int HitPoints;
        public Vector3 Position;
        public Vector3 Rotation;

        public UnitData(string type, int hitPoints, Vector3 position, Vector3 rotation)
        {
            Type = type;
            HitPoints = hitPoints;
            Position = position;
            Rotation = rotation;
        }
    }

    public struct Units
    {
        public List<UnitData> list;

        public Units(List<UnitData> list)
        {
            this.list = list;
        }
    }

    public class UnitGameSaver : GameSaver<Units, UnitManager>
    {
        private Dictionary<string, Unit> unitPrefabs = new();

        [Inject]
        private void Construct(Unit[] unitPrefabs)
        {
            for (var i = 0; i < unitPrefabs.Length; i++)
            {
                this.unitPrefabs.Add(unitPrefabs[i].Type, unitPrefabs[i]);
            }
        }

        protected override Units ConvertToData(UnitManager service)
        {
            IEnumerable<Unit> units = service.GetAllUnits();
            List<UnitData> unitList = new();
            foreach (Unit unit in units)
            {
                var unitData = new UnitData(unit.Type, unit.HitPoints, unit.Position, unit.Rotation);
                Debug.Log($"Unit {unitData.Type} was converted to data");
                unitList.Add(unitData);
            }

            return new Units(unitList);
        }

        protected override void SetupData(Units data, UnitManager service)
        {
            Unit[] currentUnits = service.GetAllUnits().ToArray();
            for (var index = 0; index < currentUnits.Length; index++)
            {
                Unit currentUnit = currentUnits[index];
                service.DestroyUnit(currentUnit);
                Debug.Log($"{currentUnit.Type} was destroyed");
            }

            foreach (UnitData unitData in data.list)
            {
                Unit unitPrefab = unitPrefabs[unitData.Type];
                unitPrefab.HitPoints = unitData.HitPoints;
                service.SpawnUnit(unitPrefab, unitData.Position, Quaternion.Euler(unitData.Rotation));
                Debug.Log($"{unitPrefab.Type} was spawned");
            }
        }
    }
}