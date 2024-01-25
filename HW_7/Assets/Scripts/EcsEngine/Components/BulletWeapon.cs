using System;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct BulletWeapon
    {
        public Transform firePoint;
        public Entity bulletPrefab;
    }
}