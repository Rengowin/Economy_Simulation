using UnityEngine;

public class StorageManager : MonoBehaviour, ITickable
{

    // eventull evalieren ob float für die ressourcen mehr sinn macht


    //Instance of GlobalStorage
    public static StorageManager Instance;
    Storage globalStorage = new Storage();

    public Storage GlobalStorage { get => globalStorage; }

    [SerializeField]
    float timeToNextDebug;
    float currentTime;

    [SerializeField]
    TickController tickController;

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
        if(currentTime >= timeToNextDebug)
        {
            ShowDebugStorage();
            currentTime = 0;
        }
        else
        {
            currentTime += deltaTime;
        }
    }

    // should show all current ressourse in the storage and their amount
    void ShowDebugStorage()
    {
        foreach (var resource in globalStorage.Resources)
        {
            Debug.Log(resource.Key.ResourceName + ": " + resource.Value);
        }
    }

}
