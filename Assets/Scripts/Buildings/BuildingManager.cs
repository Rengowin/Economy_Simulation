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
        if(!BuildingPlacementValidator.CanPlaceBuilding(buildingData, position))
        {
            Debug.Log("Cannot place building at this position!");
            return;
        }
        if (canBuild(buildingData.BuildCosts))
        {
            SpawnBuilding(buildingData.BuildingPrefab, position);
            foreach (ResourceAmount resource in buildingData.BuildCosts)
            {
                storageManager.GlobalStorage.Remove(resource.Ressource, resource.Amount);
            }
        }
    }

    public void SpawnBuilding(GameObject buildingPrefab, Vector3 position)
    {
        Instantiate(buildingPrefab, position, Quaternion.identity, buildingContainer);
    }

    bool canBuild(List<ResourceAmount> cost)
    {
        foreach (ResourceAmount resource in cost)
        {
            if (storageManager.GlobalStorage.GetAmount(resource.Ressource) < resource.Amount)
            {
                Debug.Log("not enough resources to build " + resource.Ressource.name);
                return false;
            }
        }
        return true;

        // later newer rules for building needs to be added here :D
    }
}
