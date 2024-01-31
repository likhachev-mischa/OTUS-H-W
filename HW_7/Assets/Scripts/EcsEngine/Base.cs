using Content;
using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EcsEngine
{
    public sealed class Base : MonoBehaviour
    {
        [SerializeField] private Teams team;

        [SerializeField] private Vector3 spawnPos;
        [SerializeField] private Entity archer;
        [SerializeField] private Entity swordsman;

        [Button]
        private void SpawnEntity(Entity entity, Vector3 position)
        {
            EcsAdmin.Instance.CreateEntity(EcsWorlds.Events)
                .Add(new SpawnRequest())
                .Add(new Position() { value = position })
                .Add(new Rotation() { value = Quaternion.Euler(Vector3.zero) })
                .Add(new Prefab() { value = entity })
                .Add(new Team() { value = team });
        }

        [Button]
        private void SpawnArcher()
        {
            RandomizePosition();
            SpawnEntity(archer,spawnPos);
        }

        [Button]
        private void SpawnSwordsman()
        {
            RandomizePosition();
            SpawnEntity(swordsman,spawnPos);
        }

        private void RandomizePosition()
        {
            float offset = 0;
            if (team == Teams.RED)
            {
                offset = 25f;
            }
            
            spawnPos.x = Random.Range(-5, 10);
            spawnPos.x += offset;
            spawnPos.z = Random.Range(-10, 11);
        }
    }
}