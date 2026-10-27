using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-1)]
public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    [SerializeField]
    StorageManager storageManager;

    [SerializeField]
    Transform buildingContainer;


    [Header("Building Data kommt weg sobald mehr gebäude/ui stuff kommt")]
    [SerializeField]
    BuildingData woodCutterData;
    [SerializeField]
    BuildingData sawMillData;

    [SerializeField]
    BuildingData hutData;


    public void SpawnTestSawMill(Vector3 pos)
    {
        BuildBuilding(sawMillData, pos);
    }

    public void SpawnTestWoodCutter(Vector3 pos)
    {
        BuildBuilding(woodCutterData, pos);
    }

    public void SpawnTestHut(Vector3 pos)
    {
        BuildBuilding(hutData, pos);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (storageManager == null)
        {
            storageManager = StorageManager.Instance;
        }
    }

    public void BuildBuilding(BuildingData buildingData, Vector3 position)
    {
        if (canBuild(buildingData.BuildCosts))
        {
            SpawnBuilding(buildingData.BuildingPrefab, position);
            foreach (ResourceAmount resource in buildingData.BuildCosts)
            {
                storageManager.GlobalStorage.Remove(resource.Ressource, resource.Amount);
            }
        }
        else
        {
            Debug.Log("Not enough resources to build!");
        }
    }

    void SpawnBuilding(GameObject buildingPrefab, Vector3 position)
    {
        Instantiate(buildingPrefab, position, Quaternion.identity, buildingContainer);
    }

    bool canBuild(List<ResourceAmount> cost)
    {
        foreach (ResourceAmount resource in cost)
        {
            if (storageManager.GlobalStorage.GetAmount(resource.Ressource) < resource.Amount)
            {
                return false;
            }
        }
        return true;

        // later newer rules for building needs to be added here :D
    }
}
