using System;
using DI;
using SaveSystem;
using UnityEngine;

namespace Common
{
    public class RepositoryInstaller : GameInstaller
    {
        [Service(typeof(GameRepository))] private GameRepository gameRepository = new();
    }
}