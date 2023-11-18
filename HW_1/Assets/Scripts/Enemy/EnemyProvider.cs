using UnityEngine;

namespace ShootEmUp
{
    namespace Enemy
    {
        public abstract class EnemyAgent : MonoBehaviour
        {
        }

        public sealed class EnemyProvider : MonoBehaviour
        {
            public EnemyAgent[] Agents { get; private set; }

            private void Awake()
            {
                Agents = this.GetComponents<EnemyAgent>();
            }
        }
    }
}