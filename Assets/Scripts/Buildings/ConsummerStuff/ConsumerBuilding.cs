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

    // setter for later buffs/calc of what pupulation it should be
    public int CurrentPopulation { get => currentPopulation; set => currentPopulation = value; }
    public int MaxPopulation { get => maxPopulation; set => maxPopulation = value; }


    // oder wait vlt wäre das doch lieber in pobultion manager? aber hier wäre dann ok weil wir x% happy sind oder x% nur von der ressourse wachst die befögerung auf maxed oder schrumpft leicht
    // gedanken zumachen wie man es am dümmsten anstellen kann
    public void GameTick(float deltaTime)
    {
    }

    void OnEnable()
    {
        if (populationManager == null)
        {
            populationManager = PopulationManager.Instance;
        }
        populationManager.RegisterConsumerBuilding(this);
        currentPopulation = maxPopulation; // für jetzt test
    }

    void OnDisable()
    {
        populationManager.UnregisterConsumerBuilding(this);
    }
}
