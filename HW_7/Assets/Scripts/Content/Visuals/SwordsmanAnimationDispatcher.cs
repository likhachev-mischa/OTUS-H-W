using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public class SwordsmanAnimationDispatcher : MonoBehaviour
    {
        private SwordsmanInstaller swordsmanInstaller;

        private void Awake()
        {
            swordsmanInstaller = gameObject.GetComponentInParent<SwordsmanInstaller>();
        }

        public void ReceiveEvent(string str)
        {
            if (str == "attack")
            {
                swordsmanInstaller.Hit();
            }
        }
    }
}