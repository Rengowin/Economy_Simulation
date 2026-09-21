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

    List<ConsumerBuilding> consumerBuildings = new List<ConsumerBuilding>();

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
    }

    void ShowDebugPopulation()
    {
        CalcPopulation();
        Debug.Log("Current Population: " + currentPopulation + " / " + CalcMaxPopulation());
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
