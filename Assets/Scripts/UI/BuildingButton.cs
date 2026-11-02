using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    [SerializeField]
    BuildingData buildingData;
    [SerializeField]
    InputController inputController;
    [SerializeField]
    Button button;

    void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();

        button.onClick.AddListener(SelectBuilding);
    }

    void Start()
    {
        if (inputController == null)
            inputController = InputController.Instance;
    }

    public void SelectBuilding()
    {
        inputController.SelectedBuilding = buildingData;
        Debug.Log($"Selected: {buildingData.name}");
    }
}
