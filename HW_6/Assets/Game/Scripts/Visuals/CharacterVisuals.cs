using DI;
using UnityEngine;

namespace Game
{
    public sealed class CharacterVisuals : MonoBehaviour, IGameUpdateListener, IGamePostConstructListener,
        IGameStartListener
    {
        [SerializeField] private Animator animator;
        private Character character;


        private CharacterAnimatorController animatorController;

        [Inject]
        private void Construct(Character character)
        {
            this.character = character;
        }

        public void OnPostConstruct()
        {
            animatorController = new CharacterAnimatorController(animator, character.MoveDirection, character.IsDead,
                character.FireRequest,character.transform);
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