using UnityEngine;

[System.Serializable]
public class NeedState
{
    [SerializeField]
    NeedResourceData needResourceData;
    [SerializeField]
    float accumulatedNeed;
    /*
     * for later
     * [SerializeField]
     * float fulfillment;
    */

    /*
     * enum of the population type or ScriptableObjects
     * [SerializeField]
     * populationTier or populationType;
     * because idk where i will end with this project
     */

    [SerializeField]
    int populationNeededToUnlock;

    [SerializeField]
    bool unlocked;

    public int PopulationNeededToUnlock { get => populationNeededToUnlock; }
    public bool Unlocked { get => unlocked; set => unlocked = value; }

    public NeedResourceData NeedResourceData { get => needResourceData; }
    public float AccumulatedNeed { get => accumulatedNeed; set => accumulatedNeed = value; }
}
