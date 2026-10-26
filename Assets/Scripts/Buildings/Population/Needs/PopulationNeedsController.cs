using UnityEngine;
using System.Collections.Generic;

public class PopulationNeedsController : MonoBehaviour, ITickable
{
    [SerializeField]
    TickController tickController;
    [SerializeField]
    PopulationManager populationManager;
    [SerializeField]
    StorageManager storageManager;

    [SerializeField]
    int updateInterval = 5;
    float currentTime;

    [SerializeField, Min(1)]
    int fulfillmentDurationInIntervals = 3;

    [SerializeField]
    List<PopulationNeed> populationNeeds;

    [SerializeField]
    List<NeedState> needStates = new();

    void OnEnable()
    {
        if (tickController == null)
            tickController = TickController.Instance;

        if (tickController != null)
            tickController.Register(this);
    }

    void OnDisable()
    {
        if (tickController != null)
            tickController.Unregister(this);
    }

    public void Start()
    {
        if (tickController == null)
        {
            tickController = TickController.Instance;
        }
        if (populationManager == null)
        {
            populationManager = PopulationManager.Instance;
        }
        if (storageManager == null)
        {
            storageManager = StorageManager.Instance;
        }
        tickController.Register(this);
        InitializeNeedStates();
    }

    public void GameTick(float deltaTime)
    {
        currentTime += deltaTime;
        if (currentTime >= updateInterval)
        {
            currentTime -= updateInterval;
            UpdateUnlocks();
            UpdateNeeds();
            UpdateDebug();
        }
        UpdateFulfillment(deltaTime);
    }

    void UpdateUnlocks()
    {
        foreach(NeedState needs in needStates)
        {
            if (!needs.Unlocked && populationManager.CurrentPopulation >= needs.NeedResourceData.RequiredPopulationToUnlock)
            {
                needs.Unlocked = true;
            }
        }
    }
    void UpdateNeeds()
    {
        foreach(NeedState needs in needStates)
        {
            if (needs.Unlocked)
            {
                int amountInStorage = storageManager.GlobalStorage.GetAmount(needs.NeedResourceData.Ressource);
                float consumption = CalculateResourceConsumption(needs.NeedResourceData);

                needs.AccumulatedNeed += consumption;
                needs.SupplyRatio = CalculateSupplyRatio(consumption, amountInStorage); 

                if (needs.AccumulatedNeed >= 1)
                {
                    int neededAmount = Mathf.FloorToInt(needs.AccumulatedNeed);
                    int amountToTake = HowManyWeCanTake(
                        neededAmount,
                        needs.NeedResourceData
                    );

                    needs.AccumulatedNeed -= neededAmount;

                    storageManager.GlobalStorage.Remove(
                        needs.NeedResourceData.Ressource,
                        amountToTake
                    );
                }
            }
        }
    }

    void UpdateFulfillment(float deltaTime)
    {
        float duration = fulfillmentDurationInIntervals * updateInterval;

        foreach (NeedState needs in needStates)
        {
            if (!needs.Unlocked)
                continue;

            needs.Fulfillment = Mathf.MoveTowards(
                needs.Fulfillment,
                needs.SupplyRatio,
                deltaTime / duration
            );
        }
    }

    void UpdateDebug()
    {
        foreach (NeedState needs in needStates)
        {
            if (needs.Unlocked)
            {
                Debug.Log("Need: " + needs.NeedResourceData.Ressource + " Accumulated Need: " + needs.AccumulatedNeed);
            }
        }
    }

    float CalculateResourceConsumption(NeedResourceData resource) {
        float amount = resource.AmountPerMinute
                     * (populationManager.CurrentPopulation / (float)resource.PopulationPerUnit)
                     * (updateInterval / 60f); return amount;
    }

    int HowManyWeCanTake(int neededAmount, NeedResourceData resource)
    {
        int amountInStorage = storageManager.GlobalStorage.GetAmount(resource.Ressource);
        return Mathf.Min(amountInStorage, neededAmount);
    }

    float CalculateSupplyRatio(float neededAmount, float suppliedAmount)
    {
        if (neededAmount == 0)
            return 0f;

        return Mathf.Clamp01(suppliedAmount / neededAmount);
    }

    // TODO: later after gamecontroller/ placement off buildigngs

    bool DoWeNeedToCalculate(NeedResourceData resource)
    {
        return false;
    }

    public float CalculateAverageFulfillment() {
        int unlockedNeedsCount = 0;
        float sumFulfillment = 0f;

        foreach (NeedState needs in needStates)
        {
            if (needs.Unlocked)
            {
                unlockedNeedsCount++;
                sumFulfillment += needs.Fulfillment;
            }
        }
        if(unlockedNeedsCount == 0)
            return 0f;
        return sumFulfillment / unlockedNeedsCount;
    }

    void InitializeNeedStates()
    {
        foreach (PopulationNeed tierNeed in populationNeeds)
        { 
            foreach(NeedResourceData resourceData in tierNeed.NeedResources)
            {
                needStates.Add(new NeedState(resourceData));
            }

        }
    }


    //build later a check maybe if i add savestates and add need resource needs that need needstats are created
}
