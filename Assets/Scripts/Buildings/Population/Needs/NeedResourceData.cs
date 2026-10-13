using UnityEngine;

[System.Serializable]
public class NeedResourceData
{
    [SerializeField]
    ResourceType resource;

    [SerializeField]
    float amountPerMinute = 1.0f;

    //guess needs a better name but idk one just yet
    [SerializeField]
    int populationPerUnit;

    /*
     * enum of the population type or ScriptableObjects
     * [SerializeField]
     * populationTier or populationType;
     * because idk where i will end with this project
     */

    [SerializeField]
    int requiredPopulationToUnlock;
    public int RequiredPopulationToUnlock { get => requiredPopulationToUnlock; }

    public ResourceType Ressource { get => resource; }
    public float AmountPerMinute { get => amountPerMinute; }
    public int PopulationPerUnit { get => populationPerUnit; }
}
