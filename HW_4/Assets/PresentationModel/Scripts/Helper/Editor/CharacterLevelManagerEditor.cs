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
            
        }
    }
}