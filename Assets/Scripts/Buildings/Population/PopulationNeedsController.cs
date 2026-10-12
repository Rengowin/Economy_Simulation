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
    int UpdateInterval = 5;
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
            tickController.Register(this);
        }
        if (populationManager == null)
        {
            populationManager = PopulationManager.Instance;
        }
        if (storageManager == null)
        {
            storageManager = StorageManager.Instance;
        }
        CreateNeedStates();
    }

    public void GameTick(float deltaTime)
    {
        currentTime += deltaTime;
        if (currentTime >= UpdateInterval)
        {
            currentTime -= UpdateInterval;
            UpdateUnlocks();
            UpdateNeeds();
            UpdateDebug();
        }
    }

    void UpdateUnlocks()
    {
        foreach(NeedState needs in needStates)
        {
            if (!needs.Unlocked && populationManager.CurrentPopulation >= needs.NeedResourceData.PopulationNeededToUnlock)
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
                needs.AccumulatedNeed += ClalcNeedForRessource(needs.NeedResourceData);
                if (needs.AccumulatedNeed >= 1)
                {
                    int amountToTake = Mathf.FloorToInt(needs.AccumulatedNeed);

                    if (storageManager.GlobalStorage.Remove(
                            needs.NeedResourceData.Ressource, amountToTake))
                    {
                        needs.AccumulatedNeed -= amountToTake;
                    }
                    else
                    {
                        Debug.Log("Not enough " + needs.NeedResourceData.Ressource);
                    }
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



    float ClalcNeedForRessource(NeedResourceData resource) {
        // here needs later be a check for diffrent populations tiers wish that TODO: works like in vs code with the addon TODO Tree oder so xD, oh wait no i dont need the check -.-
        float amount = resource.AmountPerMinute
                     * (populationManager.CurrentPopulation / (float)resource.PerPopulation)
                     * (UpdateInterval / 60f); return amount;
    }

    //mmm wenn tier 1 und tier 2 beide holz brauchen mache 2 unterschiedliche needstates? also weil die einen eventull doppelt soviel holz pro 100 einwohner brauchen? also unterschiedliche wertigkeit des bedurfniss
    void CreateNeedStates()
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
