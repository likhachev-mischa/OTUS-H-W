using System;
using AIModule;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public class BTNode_SetPopupActiveState : BTNode
    {
        public override string Name => "Set Popup State";

        [SerializeField, BlackboardKey]
        private ushort popup;

        [SerializeField]
        private bool value;

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(popup, out GameObject popupObject))
            {
                return BTState.FAILURE;
            }

            popupObject.SetActive(value);
            return BTState.SUCCESS;
        }
    }
}