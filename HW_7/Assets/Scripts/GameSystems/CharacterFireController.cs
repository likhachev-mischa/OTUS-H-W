using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace GameEngine
{
    public sealed class CharacterFireController : MonoBehaviour
    {
        [SerializeField]
        private Entity character;

        [SerializeField]
        private FireInput fireInput;

        private void Update()
        {
            if (this.fireInput.IsFirePressDown())
            {
                this.character.SetData(new FireRequest());
            }
        }
    }
}