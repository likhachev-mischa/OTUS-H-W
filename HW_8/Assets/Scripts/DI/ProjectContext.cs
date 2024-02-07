using System.Linq;
using Entities.Components;
using UnityEngine;

namespace DI
{
    public sealed class ProjectContext : Context
    {
        [SerializeField] private GameInstallerContainer projectInstallerContainer;

        private SceneContext sceneContext;
        private GameInstaller[] projectInstallers;

        private ObjectResolver objectResolver;
        

        public void RegisterProject()
        {
            Initialize();

            objectResolver = new ObjectResolver(serviceLocator);
            serviceLocator.BindService(typeof(IObjectResolver), objectResolver);

            projectInstallers = projectInstallerContainer.ProvideInstallers().ToArray();

            foreach (GameInstaller installer in projectInstallers)
            {
                ExtractListeners(installer);

                ExtractServices(installer);
            }
        }

        public void StartProject()
        {
            foreach (GameInstaller installer in projectInstallers)
            {
                ExtractInjectors(installer);
            }

            InjectGameObjectsOnScene();
        }

        public void RegisterScene()
        {
            sceneContext = FindObjectOfType<SceneContext>();
            serviceLocator.BindService(typeof(Context), sceneContext);
            serviceLocator.BindService(typeof(GameManager), gameManager);
            sceneContext.RegisterServices(serviceLocator, gameManager);
        }

        public void StartScene()
        {
            sceneContext.Inject();
            gameManager.Initialize();
            gameManager.LateLoad();
            gameManager.StartGame();
        }

        public void UnloadScene()
        {
            sceneContext.Unload();
        }
    }
}