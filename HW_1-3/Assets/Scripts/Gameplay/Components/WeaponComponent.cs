using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position => firePoint.position;

        public Quaternion Rotation => firePoint.rotation;

        [SerializeField] private Transform firePoint;
    }
}