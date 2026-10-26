using UnityEngine;

[System.Serializable]
public class NeedState
{
    [SerializeField]
    NeedResourceData needResourceData;

    [Header("Debug")]
    [SerializeField]
    float accumulatedNeed;
    [SerializeField]
    float fulfillment;
    [SerializeField]
    float supplyRatio;

    [SerializeField]
    bool unlocked;
    public bool Unlocked { get => unlocked; set => unlocked = value; }

    public NeedResourceData NeedResourceData { get => needResourceData; }
    public float AccumulatedNeed { get => accumulatedNeed; set => accumulatedNeed = value; }
    public float Fulfillment { get => fulfillment; set => fulfillment = value; }
    public float SupplyRatio { get => supplyRatio; set => supplyRatio = value; }

    public NeedState(NeedResourceData needResourceData)
    {
        this.needResourceData = needResourceData;
        this.unlocked = false;
        this.accumulatedNeed = 0f;
        this.fulfillment = 0f;
        this.supplyRatio = 0f;
    }
}
