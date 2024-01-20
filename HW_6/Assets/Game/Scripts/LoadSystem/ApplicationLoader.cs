using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadSystem
{
    public sealed class ApplicationLoader : MonoBehaviour
    {
        [SerializeField] private LoadingTasksConfig loadingTasksConfig;
        [SerializeField] private ProjectContext projectContext;
        [SerializeField] private int sceneId = 1;

        private void Start()
        {
            projectContext.RegisterProject();
            projectContext.StartProject();
            LoadApplication().Forget();
        }

        private async UniTaskVoid LoadApplication()
        {
            IReadOnlyList<LoadingTask> taskList = loadingTasksConfig.LoadingTasks;

            foreach (LoadingTask task in taskList)
            {
                if (task != null)
                {
                    await task.LoadTask();
                }
            }
            
        }

        public async UniTaskVoid LoadSceneAsync()
        {
            await SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Single);
            projectContext.RegisterScene();
            projectContext.StartScene();
        }
    }
}