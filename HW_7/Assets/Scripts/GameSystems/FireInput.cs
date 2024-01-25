using UnityEngine;

namespace GameEngine
{
    public sealed class FireInput : MonoBehaviour
    {
        public bool IsFirePressDown()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}