using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MVVM
{
    public abstract class GameInstallerContainer : MonoBehaviour, IGameInstallerProvider
    {
        public IEnumerable<GameInstaller> ProvideInstallers()
        {
            FieldInfo[] fields = GetType().GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            foreach (FieldInfo field in fields)
            {
                if (field.IsDefined(typeof(GameInstallerAttribute)) &&
                    field.GetValue(this) is GameInstaller gameInstaller)
                {
                    yield return gameInstaller;
                }
            }
        }
    }
}