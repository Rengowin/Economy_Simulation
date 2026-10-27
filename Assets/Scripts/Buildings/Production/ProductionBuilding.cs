using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding : MonoBehaviour, ITickable
{
    [Header("Recipe")]
    [SerializeField]
    ProductionRecipe recipe;
    [Header("TimeReference")]
    [SerializeField]
    float productionDuration;
    [SerializeField]
    float currentProductionTime;

    [Header("Storage")]
    //global storage for now/ testing until maybe pathfinding is implemented
    [SerializeField]
    StorageManager globalStorage;

    //local storage for later
    Storage localStorage;

    bool isWorking = false;

    [Header("TickController dont need to be set but can :D")]
    [SerializeField]
    TickController tickController;

    void OnEnable()
    {
        if (tickController == null)
        {
            tickController = TickController.Instance;
        }
        tickController.Register(this);
    }
    void OnDisable()
    {
        tickController.Unregister(this);
    }

    void Start()
    {
        if(tickController == null)
        {
            tickController = TickController.Instance;
            tickController.Register(this);
        }
        productionDuration = recipe.ProductionTime;
        if(globalStorage == null)
        {
            globalStorage = StorageManager.Instance;
        }
    }

    public void GameTick(float deltaTime)
    {
        if (isWorking)
        {
            currentProductionTime += deltaTime;
            //check if work is done
            if(currentProductionTime >= productionDuration)
            {
                foreach(ResourceAmount resource in recipe.Output)
                {
                    globalStorage.GlobalStorage.Add(resource.Ressource, resource.Amount);
                }
                //check if a new workflow can start
                if (CanProduce())
                {
                    foreach (ResourceAmount resource in recipe.Input)
                    {
                        globalStorage.GlobalStorage.Remove(resource.Ressource, resource.Amount);
                    }
                    currentProductionTime-=productionDuration;
                }
                else
                {
                    isWorking = false;
                    currentProductionTime = 0;
                }
            }

        }
        else if (CanProduce())
        {
            isWorking = true;
            foreach(ResourceAmount resource in recipe.Input)
            {
                globalStorage.GlobalStorage.Remove(resource.Ressource, resource.Amount);
            }
        }
    }

    bool CanProduce()
    {
        foreach(ResourceAmount resource in recipe.Input)
        {
            if (!globalStorage.GlobalStorage.Has(resource.Ressource, resource.Amount))
            {
                return false;
            }
        }
        return true;
    }
}
