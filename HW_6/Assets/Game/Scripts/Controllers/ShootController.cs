using DI;
using UnityEngine;

namespace Game
{
    public class ShootController : IGameUpdateListener,IGameLateLoadListener
    {
        private CharacterEntity characterEntity;
        private ShootComponent shootComponent;

        private bool enabled;

        [Inject]
        private void Construct(CharacterEntity characterEntity)
        {
            this.characterEntity = characterEntity;
        }

        public void OnLateLoad()
        {
            if (characterEntity.TryGet(out shootComponent))
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
            
            if (Input.GetMouseButton(0))
            {
                shootComponent.Shoot();
            }
        }
    }
}