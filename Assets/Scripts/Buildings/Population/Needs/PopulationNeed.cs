using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "new PopulationNeed", menuName = "Scriptable Objects/Population/PopulationNeed")]
public class PopulationNeed : ScriptableObject
{
    [SerializeField]
    List<NeedResourceData> needResources;

    public List<NeedResourceData> NeedResources { get => needResources; }
}
