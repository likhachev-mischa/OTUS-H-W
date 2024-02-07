using System.Collections.Generic;

namespace Pipeline
{
    public sealed class CompositeVisualTask : Task
    {
        private readonly List<Task> _tasks = new();
        
        private int _currentIndex;
        
        public CompositeVisualTask(params Task[] tasks)
        {
            foreach (Task task in tasks)
            {
                _tasks.Add(task);
            }
        }

        protected override void OnRun()
        {
            foreach (Task task in _tasks)
            {
                task.Run(OnTaskFinished);
            }
        }

        private void OnTaskFinished()
        { 
            _currentIndex++;
            if (_currentIndex >= _tasks.Count)
                Finish();
        }
    }
}