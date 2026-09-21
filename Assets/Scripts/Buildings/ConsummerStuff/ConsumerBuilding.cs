using UnityEngine;

public class ConsumerBuilding : MonoBehaviour, ITickable
{
    // mmm int oder float? wegen / später bzw short wegen daten sparen mmm idk
    [SerializeField]
    int currentPopulation;
    [SerializeField]
    int maxPopulation;
    [SerializeField]
    PopulationManager populationManager;
    [SerializeField]
    TickController tickController;

    [SerializeField]
    float timeToNextPopGrouth;
    float currentTime;

    // setter for later buffs/calc of what pupulation it should be
    public int CurrentPopulation { get => currentPopulation; set => currentPopulation = value; }
    public int MaxPopulation { get => maxPopulation; set => maxPopulation = value; }


    // oder wait vlt wäre das doch lieber in pobultion manager? aber hier wäre dann ok weil wir x% happy sind oder x% nur von der ressourse wachst die befögerung auf maxed oder schrumpft leicht
    // gedanken zumachen wie man es am dümmsten anstellen kann
    public void GameTick(float deltaTime)
    {
        if(currentTime >= timeToNextPopGrouth)
        {
            GrowPopulation();
            currentTime = 0;
        }
        else
        {
            currentTime += deltaTime;
        }
    }

    void OnEnable()
    {
        if (populationManager == null)
        {
            populationManager = PopulationManager.Instance;
        }
        populationManager.RegisterConsumerBuilding(this);
        if(tickController == null)
        {
            tickController = TickController.Instance;
        }
        tickController.Register(this);
    }

    void OnDisable()
    {
        populationManager.UnregisterConsumerBuilding(this);
        tickController.Unregister(this);
    }

    //if later population can gropw more then 1 per methode call (diffrent consumtions/ als groth idk yet)
    void GrowPopulation()
    {
        if (currentPopulation < maxPopulation)
        {
            currentPopulation++;
        }
    }
}