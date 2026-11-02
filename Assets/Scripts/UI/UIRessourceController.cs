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
    TextMeshProUGUI populationText;

    [SerializeField] ResourceType woodResource;
    [SerializeField] ResourceType planksResource;

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
        woodText.text = storageManager.GlobalStorage.GetAmount(woodResource).ToString();
        planksText.text = storageManager.GlobalStorage.GetAmount(planksResource).ToString();
        //stoneText.text = storageManager.GlobalStorage.GetAmount(ResourceType.Stone).ToString();
        populationText.text = populationManager.CurrentPopulation.ToString();
    }
}
