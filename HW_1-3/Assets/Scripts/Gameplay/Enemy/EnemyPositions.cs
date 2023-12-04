using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPositions : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPositions;

        [SerializeField] private Transform[] attackPositions;

        public Transform RandomSpawnPosition()
        {
            return RandomTransform(spawnPositions);
        }

        public Transform RandomAttackPosition()
        {
            return RandomTransform(attackPositions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            int index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}