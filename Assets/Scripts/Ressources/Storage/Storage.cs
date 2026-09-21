using System.Collections.Generic;
using UnityEngine;

public class Storage: IStorage
{
    Dictionary<RessourceEnum, int> resources = new();

    public void Add(RessourceEnum ressource, int amount)
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

    public bool Remove(RessourceEnum ressource, int amount)
    {
        if (resources.ContainsKey(ressource) && resources[ressource] >= amount)
        {
            resources[ressource] -= amount;
            return true;
        }
        return false;
    }

    public bool Has(RessourceEnum ressource, int amount)
    {
        return resources.ContainsKey(ressource) && resources[ressource] >= amount;
    }

    public int GetAmount(RessourceEnum ressource)
    {
        if (resources.ContainsKey(ressource))
        {
            return resources[ressource];
        }
        return 0;
    }    
}
