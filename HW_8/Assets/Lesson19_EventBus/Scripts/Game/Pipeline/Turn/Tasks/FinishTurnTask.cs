using UnityEngine;

namespace Lessons.Game.Pipeline.Turn.Tasks
{
    public sealed class FinishTurnTask : Task
    {
        protected override void OnRun()
        {
            Debug.Log("Finish Turn!");
            
            Finish();
        }
    }
}