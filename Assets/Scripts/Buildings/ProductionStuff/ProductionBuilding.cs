using System.Data;
using UnityEngine;

public class ProductionBuilding : MonoBehaviour, ITickable
{
    [Header("Recipe")]
    [SerializeField]
    ProductionRecipe recipe;

    [Header("BuildCost")]
    [SerializeField]
    BuildCost buildCost;

    [Header("TimeReference")]
    [SerializeField]
    float timeNeedToProduce;
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

    void Start()
    {
        if(tickController == null)
        {
            tickController = TickController.Instance;
        }
        tickController.Register(this);
        timeNeedToProduce = recipe.ProductionTime;
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
            if(currentProductionTime >= timeNeedToProduce)
            {
                foreach(ResourceAmount resource in recipe.Output)
                {
                    globalStorage.GlobalStorage.Add(resource.Ressource, resource.Amount);
                }
                //check if a new workflow can start
                if (canProduce())
                {
                    foreach (ResourceAmount resource in recipe.Input)
                    {
                        globalStorage.GlobalStorage.Remove(resource.Ressource, resource.Amount);
                    }
                    currentProductionTime-=timeNeedToProduce;
                }
                else
                {
                    isWorking = false;
                    currentProductionTime = 0;
                }
            }

        }
        else if (canProduce())
        {
            isWorking = true;
            foreach(ResourceAmount resource in recipe.Input)
            {
                globalStorage.GlobalStorage.Remove(resource.Ressource, resource.Amount);
            }
        }
    }

    bool canProduce()
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
