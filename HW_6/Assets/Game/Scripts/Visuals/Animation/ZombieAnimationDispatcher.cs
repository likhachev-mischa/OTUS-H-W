using UnityEngine;

namespace Game
{
    public class ZombieAnimationDispatcher : MonoBehaviour
    {
        [SerializeField] private Zombie zombie;

        public void ReceiveEvent(string value)
        {
            if (value == "attack")
            {
                zombie.AttackEvent.Invoke();
            }
            if (value == "reset")
            {
                zombie.CanAttack.Value = true;
            }

            if (value == "death")
            {
                zombie.Despawn.Invoke(zombie);
            }
        }
    }
}