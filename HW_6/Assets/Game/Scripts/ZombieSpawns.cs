using DI;
using UnityEngine;

namespace Game
{
    public class ZombieSpawns : MonoBehaviour
    {
        public Transform[] Positions { get; private set; }

        [Inject]
        private void Construct()
        {
            Positions = this.gameObject.GetComponentsInChildren<Transform>();
        }
    }
}