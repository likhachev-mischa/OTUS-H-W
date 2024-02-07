using System;
using DI;
using Pipeline;
using UnityEngine;

namespace Installers
{
    [Serializable]
    public sealed class PipelineInstaller : GameInstaller
    {
        [Service(typeof(VisualPipeline))] private VisualPipeline visualPipeline = new();
        [Service(typeof(TurnPipeline))] private TurnPipeline turnPipeline = new();
        [Listener] private TurnPipelineInstaller turnPipelineInstaller = new();
        [SerializeField] [Listener] private TurnPipelineRunner turnPipelineRunner = new();
    }
}