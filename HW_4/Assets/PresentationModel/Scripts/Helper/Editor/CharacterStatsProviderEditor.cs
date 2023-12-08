using UnityEditor;
using UnityEngine;

namespace MVVM
{
    [CustomEditor(typeof(CharacterStatsProvider))]
    public class CharacterStatsProviderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var provider = (CharacterStatsProvider)target;


            if (GUILayout.Button("Add Stat"))
            {
                provider.AddStat();
            }

            if (GUILayout.Button("Remove Stat"))
            {
                provider.RemoveStat();
            }
        }
    }
}