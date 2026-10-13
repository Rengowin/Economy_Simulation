using UnityEngine;

public interface IStorage
{
    void Add(ResourceType ressource, int amount);
    bool Remove(ResourceType ressource, int amount);
    bool Has(ResourceType ressource, int amount);
    int GetAmount(ResourceType ressource);

}
