using System;
using System.Collections.Generic;
using System.Text;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    [Serializable]
    public sealed class CurrencyRepository : ISaveLoader
    {
        private static readonly string CURRENCY_CONTAINER_SAVE_ID = "CurrencyContainer";

        [ShowInInspector, ReadOnly] private Dictionary<string, int> currencyContainer;

        public CurrencyRepository(string[] currencies)
        {
            currencyContainer = new Dictionary<string, int>(currencies.Length);

            for (var i = 0; i < currencies.Length; i++)
            {
                currencyContainer.Add(currencies[i], 0);
            }
        }

        public void AddToCurrency(string id, int value)
        {
            currencyContainer[id] += value;
        }

        public void OnLoadGame()
        {
            var str = PlayerPrefs.GetString(CURRENCY_CONTAINER_SAVE_ID);

            if (str.Length == 0)
            {
                return;
            }

            currencyContainer = new Dictionary<string, int>();

            while (str.Length != 0)
            {
                string key;
                string value;
                (key, value) = ParseString(ref str);
                currencyContainer.Add(key, int.Parse(value));
            }
        }


        public void OnSaveGame()
        {
            StringBuilder sb = new();
            foreach ((string key, int value) in currencyContainer)
            {
                string str = $"{key} {value}\n";
                sb.Append(str);
            }

            PlayerPrefs.SetString(CURRENCY_CONTAINER_SAVE_ID, sb.ToString());
        }


        private static (string key, string value) ParseString(ref string str)
        {
            int sectionEnd = str.IndexOf(' ');
            string key = str.Substring(0, sectionEnd);
            str = str.Remove(0, sectionEnd + 1);
            sectionEnd = str.IndexOf('\n');
            string value = str.Substring(0, sectionEnd);
            str = str.Remove(0, sectionEnd + 1);
            return (key, value);
        }
    }
}