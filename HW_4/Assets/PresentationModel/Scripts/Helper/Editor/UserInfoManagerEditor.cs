using UnityEditor;
using UnityEngine;

namespace MVVM
{
    [CustomEditor(typeof(UserInfoManager))]
    public sealed class UserInfoManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var manager = (UserInfoManager)target;


            if (GUILayout.Button("Change All Info From Config"))
            {
                manager.ChangeUserInfo();
            }

            if (GUILayout.Button("Change Name"))
            {
                manager.ChangeName();
            }

            if (GUILayout.Button("Change Description"))
            {
                manager.ChangeDescription();
            }

            if (GUILayout.Button("Change Icon"))
            {
                manager.ChangeIcon();
            }
        }
    }
}