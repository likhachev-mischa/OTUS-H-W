using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        [SerializeField] private ServiceLocator serviceLocator;

        [SerializeField] private MonoBehaviour[] installers;

        private void Awake()
        {
            foreach (var installer in this.installers)
            {
                if (installer is IGameListenerProvider listenerProvider)
                {
                    this.gameManager.AddListeners(listenerProvider.ProvideListeners());
                }

                if (installer is IServiceProvider serviceProvider)
                {
                    var services = serviceProvider.ProvideServices();
                    foreach (var (type, service) in services)
                    {
                        this.serviceLocator.BindService(type, service);
                    }
                }
            }
        }

        private void Start()
        {
            foreach (var installer in this.installers)
            {
                if (installer is IInjectProvider injectProvider)
                {
                    injectProvider.Inject(this.serviceLocator);
                }
            }

            this.InjectGameObjectsOnScene();
        }

        private void InjectGameObjectsOnScene()
        {
            GameObject[] gameObjects = this.gameObject.scene.GetRootGameObjects();

            foreach (var go in gameObjects)
            {
                this.Inject(go.transform);
            }
        }

        private void Inject(Transform targetTransform)
        {
            var targets = targetTransform.GetComponents<MonoBehaviour>();
            foreach (var target in targets)
            {
                DependencyInjector.Inject(target, this.serviceLocator);
            }

            foreach (Transform child in targetTransform)
            {
                this.Inject(child);
            }
        }
    }
}
