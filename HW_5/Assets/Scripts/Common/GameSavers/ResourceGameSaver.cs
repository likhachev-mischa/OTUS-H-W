using System.Collections.Generic;
using DI;
using GameEngine;
using SaveSystem;

namespace Common.GameSavers
{
    public struct ResourceData
    {
        public string ID;
        public int Amount;

        public ResourceData(string id, int amount)
        {
            ID = id;
            Amount = amount;
        }
    }


    public class ResourceGameSaver : GameSaver<IEnumerable<ResourceData>, ResourceService>
    {
        protected override IEnumerable<ResourceData> ConvertToData(ResourceService service)
        {
            IEnumerable<Resource> resources = service.GetResources();

            foreach (Resource resource in resources)
            {
                yield return new ResourceData(resource.ID, resource.Amount);
            }
        }

        protected override void SetupData(IEnumerable<ResourceData> data, ResourceService service)
        {
        }
    }
}