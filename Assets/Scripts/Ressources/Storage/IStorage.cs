using UnityEngine;

public interface IStorage
{
    void Add(RessourceEnum ressource, int amount);
    bool Remove(RessourceEnum ressource, int amount);
    bool Has(RessourceEnum ressource, int amount);
    int GetAmount(RessourceEnum ressource);

}
