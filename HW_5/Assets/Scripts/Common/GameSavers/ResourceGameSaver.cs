using System.Collections.Generic;
using GameEngine;
using SaveSystem;

namespace GameSavers
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
    
    public struct Resources
    {
        public List<ResourceData> list;

        public Resources(List<ResourceData> list)
        {
            this.list = list;
        }
    }

    public class ResourceGameSaver : GameSaver<Resources, ResourceService>
    {
        protected override Resources ConvertToData(ResourceService service)
        {
            List<ResourceData> resourceList = new();
            IEnumerable<Resource> resources = service.GetResources();

            foreach (Resource resource in resources)
            {
                resourceList.Add(new ResourceData(resource.ID, resource.Amount));
            }

            return new Resources(resourceList);
        }

        protected override void SetupData(Resources data, ResourceService service)
        {
            IEnumerable<Resource> resources = service.GetResources();
            foreach (Resource resource in resources)
            {
                resource.Amount = data.list.Find(x => x.ID == resource.ID).Amount;
            }
        }
    }
}