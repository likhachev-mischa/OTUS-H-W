using System.Collections.Generic;
using UnityEngine;

namespace LoadSystem
{
    [CreateAssetMenu(menuName = "Configs/System/Loading Tasks", fileName = "LoadingTasksConfig", order = 0)]
    public class LoadingTasksConfig : ScriptableObject
    {
        [SerializeField] private LoadingTask[] loadingTasks;
        
        public IReadOnlyList<LoadingTask> LoadingTasks
        {
            get => loadingTasks;
        }
    }
}