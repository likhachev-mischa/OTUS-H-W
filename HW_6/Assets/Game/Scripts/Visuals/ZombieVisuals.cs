using UnityEngine;

namespace Game
{
    public sealed class ZombieVisuals : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Zombie zombie;

        private ZombieAnimatorController animatorController;

        public void OnEnable()
        {
            animatorController = new ZombieAnimatorController(animator, zombie.MoveDirection, zombie.IsDead,
                zombie.AttackRequest, zombie.transform, zombie.CanAttack,zombie.Despawn);
            
            animatorController.OnEnable();
        }

        public void Update()
        {
            animatorController.Update();
        }
        
        private void OnDisable()
        {
            animatorController.OnDisable();
        }
    }
}