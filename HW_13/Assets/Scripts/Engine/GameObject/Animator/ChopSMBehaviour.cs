using UnityEngine;

namespace Game.Engine
{
    public sealed class ChopSMBehaviour : StateMachineBehaviour
    {
        public bool IsChopping { get; private set; }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this.IsChopping = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this.IsChopping = false;
        }
    }
}
