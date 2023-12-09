using UnityEditor;
using UnityEngine;

namespace MVVM
{
    [CustomEditor(typeof(CharacterStatsManager))]
    public class CharacterStatsManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var manager = (CharacterStatsManager)target;


            if (GUILayout.Button("Add Stat To Character"))
            {
                manager.AddStat();
            }

            if (GUILayout.Button("Remove Stat From Character"))
            {
                manager.RemoveStat();
            }
            
            if (GUILayout.Button("Change Value Of Character's Stat"))
            {
                manager.ChangeValue();
            }
            
        }
    }
}