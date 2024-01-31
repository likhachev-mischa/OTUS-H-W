using UnityEngine;

namespace Content
{
    public class ArcherAnimationDispatcher : MonoBehaviour
    {
        private ArcherInstaller archerInstaller;

        private void Awake()
        {
            archerInstaller = gameObject.GetComponentInParent<ArcherInstaller>();
        }

        public void ReceiveEvent(string str)
        {
            if (str == "attack")
            {
                archerInstaller.LaunchArrow();
            }
        }
    }
}