using Unity.VisualScripting;
using UnityEngine;

public class BuildingPreview : MonoBehaviour
{
    [SerializeField]
    Material previewMaterial;

    [SerializeField]
    Color canBuildColor = Color.green.WithAlpha(0.4f);
    [SerializeField]
    Color cannotBuildColor = Color.red.WithAlpha(0.4f);

    public void ShowPreview(BuildingData buildingData, Vector3 position)
    {

    }

    void Update()
    {
        InputController.Instance.TryGetMouseWorldPosition(out Vector3 mouseWorldPosition);

    }

}
