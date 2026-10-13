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

    [SerializeField]
    bool unlocked;
    public bool Unlocked { get => unlocked; set => unlocked = value; }

    public NeedResourceData NeedResourceData { get => needResourceData; }
    public float AccumulatedNeed { get => accumulatedNeed; set => accumulatedNeed = value; }


    public NeedState(NeedResourceData needResourceData)
    {
        this.needResourceData = needResourceData;
        this.unlocked = false;
        this.accumulatedNeed = 0f;
    }
}
