using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace GameEngine
{
    internal sealed class CharacterMoveController : MonoBehaviour
    {
        [SerializeField]
        private Entity character;

        [SerializeField]
        private MoveInput input;
        
        private void Update()
        {
            Vector3 inputDirection = this.input.GetDirection();
            ref MoveDirection moveDirection = ref this.character.GetData<MoveDirection>();
            moveDirection.value = inputDirection;
        }
    }
}