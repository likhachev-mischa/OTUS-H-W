﻿using UnityEngine;

namespace MVVM
{
    [CreateAssetMenu(menuName = "Confgis/User Info", fileName = "User Info")]
    public class UserInfoConfig : ScriptableObject
    {
        public string userName;
        public string description;
        public Sprite icon;
    }
}