using UnityEngine;
using System.Collections.Generic;
using System;

[Obsolete("This class is deprecated. Use BuildingData instead.")]
[CreateAssetMenu(fileName = "new BuildCost", menuName = "Scriptable Objects/BuildCost")]
public class BuildCost : ScriptableObject
{
    [SerializeField]
    List<ResourceAmount> cost;

    public List<ResourceAmount> Cost { get => cost; }


    // should go into the buldingData class
}
