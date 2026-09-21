using UnityEngine;
using System.Collections.Generic;

public class PopulationManager : MonoBehaviour, ITickable
{
    public static PopulationManager Instance { get; private set; }

    [SerializeField]
    float timeToNextDebug;
    float currentTime;

    [SerializeField]
    TickController tickController;

    [SerializeField]
    int currentPopulation;

    [SerializeField, Range(0, 1)]
    float growthRate;

    //später zum scriptableObject machen jetzt enum nur wegen testen
    [SerializeField]
    RessourceEnum ressouceToEat;

    [SerializeField]
    StorageManager storageManager;

    List<ConsumerBuilding> consumerBuildings = new List<ConsumerBuilding>();


    public float GrowthRatePercentage { get => growthRate; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        if (tickController == null)
        {
            tickController = TickController.Instance;
        }
        tickController.Register(this);
        if(storageManager == null)
        {
            storageManager = StorageManager.Instance;
        }
    }

    public void GameTick(float deltaTime)
    {
        if (currentTime >= timeToNextDebug)
        {
            ShowDebugPopulation();
            currentTime = 0;
        }
        else
        {
            currentTime += deltaTime;
        }
        CalcPopulation();
        CalcWhatsNeeded();
    }

    void ShowDebugPopulation()
    {
        Debug.Log("Current Population: " + currentPopulation + " / " + CalcMaxPopulation());
        Debug.Log(
            "Population: " + currentPopulation +
            " | Wood: " + storageManager.GlobalStorage.GetAmount(ressouceToEat) +
            " | Growth Rate: " + growthRate * 100 + "%"
        );
    }

    int CalcMaxPopulation()
    {
        int maxPopulation = 0;
        foreach (ConsumerBuilding building in consumerBuildings)
        {
            maxPopulation += building.MaxPopulation;
        }
        return maxPopulation;
    }

    void CalcPopulation()
    {
        currentPopulation = 0;
        foreach (ConsumerBuilding building in consumerBuildings)
        {
            currentPopulation += building.CurrentPopulation;
        }
    }

    // right now just the one ressource later for every ressources that maybe needed (but will also look for the individaul resources)
    void CalcWhatsNeeded()
    {
        float neededAmount = currentPopulation;
        float currentAmount = storageManager.GlobalStorage.GetAmount(ressouceToEat);
        //checken für nan/null also wenn durch 0 geteilt wird oder so
        if (neededAmount == 0)
        {
            growthRate = 1;
        }
        else
        {
            growthRate = Mathf.Clamp01(currentAmount / neededAmount);
        }

    }


    public void RegisterConsumerBuilding(ConsumerBuilding building)
    {
        if (!consumerBuildings.Contains(building))
        {
            consumerBuildings.Add(building);
        }
    }

    public void UnregisterConsumerBuilding(ConsumerBuilding building)
    {
        if (consumerBuildings.Contains(building))
        {
            consumerBuildings.Remove(building);
        }
    }
}
