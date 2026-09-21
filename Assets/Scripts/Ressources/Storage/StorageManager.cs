using UnityEngine;

public class StorageManager : MonoBehaviour, ITickable
{
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
        foreach (RessourceEnum ressource in System.Enum.GetValues(typeof(RessourceEnum)))
        {
            Debug.Log(ressource + ": " + globalStorage.GetAmount(ressource));
        }
    }

}
