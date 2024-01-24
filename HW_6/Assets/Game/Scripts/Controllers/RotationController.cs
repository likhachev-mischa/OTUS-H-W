using DI;
using UnityEngine;

namespace Game
{
    public class RotationController : IGameUpdateListener,IGameLateLoadListener
    {
        private Entity characterEntity;
        private RotationComponent rotationComponent;
        private Camera camera;
        private readonly float planeDistance = 15f;

        private Plane plane;
        
        private bool enabled;

        [Inject]
        private void Construct(CharacterEntity characterEntity, Camera camera)
        {
            this.characterEntity = characterEntity;
            this.camera = camera;
        }

        public void OnLateLoad()
        {
            if (characterEntity.TryGet(out rotationComponent))
            {
                enabled = true;

                Vector3 distanceFromCamera = new Vector3(camera.transform.position.x, camera.transform.position.y - planeDistance,
                    camera.transform.position.z);

                plane = new Plane(Vector3.up, distanceFromCamera);
            }
        }

        public void OnUpdate(float deltaTime)
        {
            if (!enabled)
            {
                return;
            }

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Vector3 hitPoint = Vector3.zero;
            if (plane.Raycast(ray, out float enter))
            {
                hitPoint = ray.GetPoint(enter);
            }
            rotationComponent.Rotate(hitPoint);
            
        }
    }
}