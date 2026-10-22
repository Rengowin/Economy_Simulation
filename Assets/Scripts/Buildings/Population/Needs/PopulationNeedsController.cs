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


    [SerializeField]
    List<PopulationNeed> populationNeeds;

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
                needs.AccumulatedNeed += CalculateResourceConsumption(needs.NeedResourceData);
                if (needs.AccumulatedNeed >= 1)
                {
                    int amountToTake = HowManyWeCanTake(Mathf.FloorToInt(needs.AccumulatedNeed), needs.NeedResourceData);
                    needs.AccumulatedNeed -= Mathf.FloorToInt(needs.AccumulatedNeed);
                    storageManager.GlobalStorage.Remove(needs.NeedResourceData.Ressource, amountToTake);

                    Debug.Log("Taking " + amountToTake + " of " + needs.NeedResourceData.Ressource);
                    Debug.Log("Accumulated Need still left: " + needs.AccumulatedNeed);
                }
            }
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

    float CalculateSupplyRatio(int neededAmount, int suppliedAmount)
    {
        if (neededAmount == 0)
            return 0f;

        return Mathf.Clamp01(suppliedAmount / (float)neededAmount);
    }

    // TODO: later after gamecontroller/ placement off buildigngs

    bool DoWeNeedToCalculate(NeedResourceData resource)
    {
        return false;
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
