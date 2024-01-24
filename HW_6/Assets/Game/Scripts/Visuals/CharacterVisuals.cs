using DI;
using UnityEngine;

namespace Game
{
    public sealed class CharacterVisuals : MonoBehaviour, IGameUpdateListener, IGamePostConstructListener,
        IGameStartListener
    {
        [SerializeField] private Animator animator;
        private Character character;

        private readonly AtomicVariable<bool> canPlayAnimation = new();

        private CharacterAnimatorController animatorController;

        [Inject]
        private void Construct(Character character)
        {
            this.character = character;
        }

        public void OnPostConstruct()
        {
            canPlayAnimation.Value = true;
            
            animatorController = new CharacterAnimatorController(animator, character.MoveDirection, character.IsDead,
                character.FireRequest, character.transform, character.CanShoot, canPlayAnimation);
            
            var dispatcher = animator.gameObject.AddComponent<CharacterAnimationDispatcher>();
            dispatcher.Initialize(character.FireEvent, canPlayAnimation);
        }

        public void OnStart()
        {
            animatorController.OnEnable();
        }

        private void OnDisable()
        {
            animatorController.OnDisable();
        }

        public void OnUpdate(float deltaTime)
        {
            animatorController.Update();
        }
    }
}