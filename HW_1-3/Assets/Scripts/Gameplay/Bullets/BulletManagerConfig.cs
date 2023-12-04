using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "BulletManagerConfig",
        menuName = "Configs/New BulletManagerConfig"
    )]
    
    public sealed class BulletManagerConfig : ScriptableObject
    {
        [SerializeField] public int initialCount = 50;
        [SerializeField] public GameObject prefab;
    }
}