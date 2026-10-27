using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    [SerializeField]
    GameObject buildingPrefab;
    [SerializeField]
    List<ResourceAmount> buildCosts;
    public GameObject BuildingPrefab => buildingPrefab;
    public List<ResourceAmount> BuildCosts => buildCosts;
}
