using System;
using Entities;
using Entities.Components;
using Events.Effects;
using Game.EventBus;

namespace Events.Requests
{
    [Serializable]
    public struct DefenceFinishedRequest : IEffect
    {
        public  IEntity Source { get; set; }
        public  Target Target { get; set; }
        
    }
}