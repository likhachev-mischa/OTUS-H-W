using UnityEngine;

namespace Pipeline.Tasks
{
    public sealed class StartTurnTask : Task
    {
        protected override void OnRun()
        {
            Debug.Log("Start Turn!");

            Finish();
        }
    }
}