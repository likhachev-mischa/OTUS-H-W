using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        protected const float SPACE_HEIGHT = 10.0f;

        [SerializeField]
        public string id;
        
        [Range(2, 99)]
        [SerializeField]
        public int maxLevel = 2;

        [Space(SPACE_HEIGHT)]
        [SerializeField]
        private PriceTable priceTable;

        public abstract Upgrade InstantiateUpgrade();

        private void OnValidate()
        {
            try
            {
                this.Validate();
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        protected virtual void Validate()
        {
            this.priceTable.OnValidate(this.maxLevel);
        }
        
        
        public int GetPrice(int level)
        {
            return this.priceTable.GetPrice(level);
        }

        [Serializable]
        public sealed class PriceTable
        {
            [Space]
            [SerializeField]
            private int basePrice;

            [Space]
            [ListDrawerSettings(OnBeginListElementGUI = "DrawLevels")]
            [SerializeField]
            private int[] levels;

            public int GetPrice(int level)
            {
                var index = level - 1;
                index = Mathf.Clamp(index, 0, this.levels.Length - 1);
                return this.levels[index];
            }

            private void DrawLevels(int index)
            {
                GUILayout.Space(8);
                GUILayout.Label($"Level #{index + 1}");
            }
        
            public void OnValidate(int maxLevel)
            {
                this.EvaluatePriceTable(maxLevel);
            }

            private void EvaluatePriceTable(int maxLevel)
            {
                var table = new int[maxLevel];
                table[0] = new int();
                for (var level = 2; level <= maxLevel; level++)
                {
                    var price = this.basePrice * level;
                    table[level - 1] = price;
                }

                this.levels = table;
            }
        }
    }
}