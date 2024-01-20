using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LoadSystem
{
    public abstract class LoadingTask : MonoBehaviour
    {
        public abstract UniTask LoadTask();
    }
}