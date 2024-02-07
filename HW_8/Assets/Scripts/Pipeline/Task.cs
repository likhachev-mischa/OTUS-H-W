using System;

namespace Pipeline
{
    public abstract class Task
    {
        private event Action Callback;
        
        public void Run(Action callback)
        {
            Callback = callback;
            OnRun();
        }

        protected abstract void OnRun();

        protected void Finish()
        {
            if (Callback is not null)
            {
                Action cachedCallback = Callback;
                Callback = null;
                
                cachedCallback.Invoke();
            }
            
            OnFinish();
        }

        protected virtual void OnFinish()
        {
            
        }

        public void Skip()
        {
            Finish();
        }
    }
}