using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    [SerializeField]
    GameObject buildingPrefab;

    [SerializeField]
    BuildCost buildCost;

    public GameObject BuildingPrefab => buildingPrefab;
    public BuildCost BuildCost => buildCost;
}
