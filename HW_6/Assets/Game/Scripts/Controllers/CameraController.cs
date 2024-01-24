using System;
using DI;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class CameraController : IGameLateUpdateListener
    {
        [SerializeField] private float speed;
        [SerializeField] private float minimumMovementDistance = 2f;

        private Character character;
        private Camera camera;

        [Inject]
        private void Construct(Character character,Camera camera)
        {
            this.character = character;
            this.camera = camera;
        }


        public void OnLateUpdate(float deltaTime)
        {
            Vector3 cameraPos = camera.transform.position;
            Vector3 characterPos = character.transform.position;
            Vector3 direction = new Vector3(characterPos.x - cameraPos.x, 0, characterPos.z - cameraPos.z);

            if (direction.magnitude > minimumMovementDistance)
            {
                camera.transform.position += direction.normalized * (speed * deltaTime);
            }
            
            

        }
    }
}