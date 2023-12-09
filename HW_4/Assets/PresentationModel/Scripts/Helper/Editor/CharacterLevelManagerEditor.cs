using UnityEditor;
using UnityEngine;

namespace MVVM
{
    [CustomEditor(typeof(CharacterLevelManager))]
    public class CharacterLevelManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var manager = (CharacterLevelManager)target;


            if (GUILayout.Button("Add Experience To Character"))
            {
                manager.AddExperience();
            }

            if (GUILayout.Button("Force Level Up Character"))
            {
                manager.ForceLevelUp();
            }
            
            manager.IsAutoLevelUpEnabled = GUILayout.Toggle(manager.IsAutoLevelUpEnabled, "Enable Auto LevelUp");
        }
    }
}