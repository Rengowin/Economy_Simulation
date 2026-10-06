using UnityEngine;

[System.Serializable]
public class NeedResourceData
{
    [SerializeField]
    RessourceEnum ressource;

    [SerializeField]
    float amountPerMinute = 1.0f;

    //guess needs a better name but idk one just yet
    [SerializeField]
    int perPopulation;

    public RessourceEnum Ressource { get => ressource; }
    public float AmountPerMinute { get => amountPerMinute; }
    public int PerPopulation { get => perPopulation; }
}
