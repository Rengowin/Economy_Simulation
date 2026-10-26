using NUnit.Framework;
using TMPro;
using UnityEngine;

public class UIRessourceController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI woodText;
    [SerializeField]
    TextMeshProUGUI planksText;
    [SerializeField]
    TextMeshProUGUI stoneText;
    [SerializeField]
    TextMeshProUGUI populationText;

    [SerializeField]
    StorageManager storageManager;

    [SerializeField]
    PopulationManager populationManager;
    void Start()
    {
        if(storageManager == null)
        {
            storageManager = StorageManager.Instance;
        }
        if(populationManager == null)
        {
            populationManager = PopulationManager.Instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        woodText.text = storageManager.GlobalStorage.GetAmount(ResourceType.Wood).ToString();
        planksText.text = storageManager.GlobalStorage.GetAmount(ResourceType.Planks).ToString();
        stoneText.text = storageManager.GlobalStorage.GetAmount(ResourceType.Stone).ToString();
        populationText.text = populationManager.CurrentPopulation.ToString();
    }
}
