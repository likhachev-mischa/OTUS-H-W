using System.Collections.Generic;
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
            projectContext.ResolveDependencies();
            LoadApplication().Forget();
        }

        private async UniTaskVoid LoadApplication()
        {
            IReadOnlyList<LoadingTask> taskList = loadingTasksConfig.LoadingTasks;

            foreach (LoadingTask task in taskList)
            {
                await task.LoadTask();
            }

            LoadScene().Forget();
        }

        private async UniTaskVoid LoadScene()
        {
            await SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(0);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneId));
        }
    }
}