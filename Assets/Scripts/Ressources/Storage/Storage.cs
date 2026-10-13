using System.Collections.Generic;
using UnityEngine;

public class Storage: IStorage
{
    Dictionary<ResourceType, int> resources = new();

    public void Add(ResourceType ressource, int amount)
    {
        if (resources.ContainsKey(ressource))
        {
            resources[ressource] += amount;
        }
        else
        {
            resources[ressource] = amount;
        }
    }

    public bool Remove(ResourceType ressource, int amount)
    {
        if (resources.ContainsKey(ressource) && resources[ressource] >= amount)
        {
            resources[ressource] -= amount;
            return true;
        }
        return false;
    }

    public bool Has(ResourceType ressource, int amount)
    {
        return resources.ContainsKey(ressource) && resources[ressource] >= amount;
    }

    public int GetAmount(ResourceType ressource)
    {
        if (resources.ContainsKey(ressource))
        {
            return resources[ressource];
        }
        return 0;
    }    
}
