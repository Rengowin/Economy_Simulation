using UnityEngine;
using System.Collections.Generic;

public class PopulationManager : MonoBehaviour, ITickable
{
    public static PopulationManager Instance { get; private set; }

    [SerializeField]
    float debugInterval;
    float currentTime;

    [SerializeField]
    TickController tickController;

    [SerializeField]
    int currentPopulation;

    [SerializeField, Range(0, 1)]
    float growthRate;

    List<ResidentialBuilding> residentialBuildings = new List<ResidentialBuilding>();

    [SerializeField]
    PopulationNeedsController populationNeedsController;


    public float GrowthRatePercentage { get => growthRate; }

    public int CurrentPopulation { get => currentPopulation; }

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

        if (populationNeedsController == null)
        {
            Debug.LogError("PopulationNeedsController is missing!", this);
        }
    }

    public void GameTick(float deltaTime)
    {
        if (currentTime >= debugInterval)
        {
            ShowDebugPopulation();
            currentTime = 0;
        }
        else
        {
            currentTime += deltaTime;
        }
        CalculatePopulation();
        CalculateGrowthRate();
    }

    void ShowDebugPopulation()
    {
        Debug.Log("Current Population: " + currentPopulation + " / " + CalculateMaxPopulation());
        Debug.Log(
            "Population: " + currentPopulation +
            " | Growth Rate: " + growthRate * 100 + "%"
        );
    }

    int CalculateMaxPopulation()
    {
        int maxPopulation = 0;
        foreach (ResidentialBuilding building in residentialBuildings)
        {
            maxPopulation += building.MaxPopulation;
        }
        return maxPopulation;
    }

    void CalculatePopulation()
    {
        currentPopulation = 0;
        foreach (ResidentialBuilding building in residentialBuildings)
        {
            currentPopulation += building.CurrentPopulation;
        }
    }

    // right now just the one ressource later for every ressources that maybe needed (but will also look for the individaul resources)
    void CalculateGrowthRate()
    {
        growthRate = populationNeedsController.CalculateAverageFulfillment();
    }


    public void RegisterResidentialBuilding(ResidentialBuilding building)
    {
        if (!residentialBuildings.Contains(building))
        {
            residentialBuildings.Add(building);
        }
    }

    public void UnregisterResidentialBuilding(ResidentialBuilding building)
    {
        if (residentialBuildings.Contains(building))
        {
            residentialBuildings.Remove(building);
        }
    }
}
