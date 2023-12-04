using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "EnemyManagerConfig",
        menuName = "Configs/New EnemyManagerConfig"
    )]
    public sealed class EnemyManagerConfig : ScriptableObject
    {
        [SerializeField] public int initialCount = 7;
        [SerializeField] public GameObject prefab;
        [SerializeField] public float spawnDelay = 5;
    }
}