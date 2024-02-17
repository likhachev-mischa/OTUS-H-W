using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(menuName = "Configs/DependentUpgradeConfig", fileName = "New DependentUpgradeConfig")]
    public class DependentUpgradeConfig : UpgradeConfig
    {
        [Space] [PropertyOrder(3)] [SerializeField]
        public Dictionary<string,int> dependencies;

        public override Upgrade InstantiateUpgrade()
        {
            return new DependentUpgrade(this);
        }
    }
}