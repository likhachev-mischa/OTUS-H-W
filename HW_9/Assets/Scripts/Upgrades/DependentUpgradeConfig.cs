using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(menuName = "Configs/DependentUpgradeConfig", fileName = "New DependentUpgradeConfig", order = 0)]
    public class DependentUpgradeConfig : UpgradeConfig
    {
        [Space] [PropertyOrder(3)] [SerializeField]
        public string[] dependantIds;

        public override Upgrade InstantiateUpgrade()
        {
            return new DependentUpgrade(this);
        }
    }
}