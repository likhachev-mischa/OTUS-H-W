using System.Collections.Generic;
using DI;
using GameEngine;
using SaveSystem;
using UnityEngine;

namespace Common.GameSavers
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

    public class UnitGameSaver : GameSaver<IEnumerable<UnitData>, UnitManager>
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

        protected override IEnumerable<UnitData> ConvertToData(UnitManager service)
        {
            IEnumerable<Unit> units = service.GetAllUnits();
            foreach (Unit unit in units)
            {
                var unitData = new UnitData(unit.Type, unit.HitPoints, unit.Position, unit.Rotation);
                yield return unitData;
            }
        }

        protected override void SetupData(IEnumerable<UnitData> data, UnitManager service)
        {
            IEnumerable<Unit> currentUnits = service.GetAllUnits();
            foreach (Unit currentUnit in currentUnits)
            {
                service.DestroyUnit(currentUnit);
            }
            
            foreach (UnitData unitData in data)
            {
                Unit unitPrefab = unitPrefabs[unitData.Type];
                unitPrefab.HitPoints = unitData.HitPoints;
                service.SpawnUnit(unitPrefab, unitData.Position, Quaternion.Euler(unitData.Rotation));
            }
        }
    }
}