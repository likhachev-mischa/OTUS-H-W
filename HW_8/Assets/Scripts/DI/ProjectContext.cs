using System.Linq;
using UnityEngine;

namespace DI
{
    public sealed class ProjectContext : Context
    {
        [SerializeField] private GameInstallerContainer projectInstallerContainer;

        private SceneContext sceneContext;
        private GameInstaller[] projectInstallers;
        
        public void RegisterProject()
        {
            Initialize();
            
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
            serviceLocator.BindService(typeof(Context),sceneContext);
            serviceLocator.BindService(typeof(GameManager),gameManager);
            sceneContext.RegisterServices(serviceLocator,gameManager);
        }

        public void StartScene()
        {
            sceneContext.Inject();
            gameManager.PostConstruct();
            gameManager.LateLoad();
            gameManager.StartGame();
        }

        public void UnloadScene()
        {
            sceneContext.Unload();
        }

        
    }
}