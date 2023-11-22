using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position => this.firePoint.position;

        public Quaternion Rotation => this.firePoint.rotation;

        [SerializeField] private Transform firePoint;
    }
}