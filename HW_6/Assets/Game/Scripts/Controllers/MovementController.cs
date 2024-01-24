using DI;
using UnityEngine;

namespace Game
{
    public class MovementController : IGameUpdateListener,IGameLateLoadListener
    {
        private Entity characterEntity;
        private MovementComponent movementComponent;

        private bool enabled;

        [Inject]
        private void Construct(CharacterEntity characterEntity)
        {
            this.characterEntity = characterEntity;
        }

        public void OnLateLoad()
        {
            if (characterEntity.TryGet(out movementComponent))
            {
                enabled = true;
            }
        }

        public void OnUpdate(float deltaTime)
        {
            if (!enabled)
            {
                return;
            }

            int z = Input.GetKey(KeyCode.W) ? 1 : 0;
            z = Input.GetKey(KeyCode.S) ? -1 : z;
            
            int x = Input.GetKey(KeyCode.D) ? 1 : 0;
            x = Input.GetKey(KeyCode.A) ? -1 : x;
            
            Vector3 moveDirection = new(x, 0, z);
            movementComponent.Move(moveDirection.normalized);
        }
    }
}