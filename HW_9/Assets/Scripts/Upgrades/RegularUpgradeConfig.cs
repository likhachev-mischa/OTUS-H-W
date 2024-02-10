using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(menuName = "Configs/RegularUpgradeConfig", fileName = "New RegularUpgradeConfig")]
    public class RegularUpgradeConfig : UpgradeConfig
    {
        public override Upgrade InstantiateUpgrade()
        {
            return new RegularUpgrade(this);
        }
    }
}