using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "new BuildCost", menuName = "Scriptable Objects/BuildCost")]
public class BuildCost : ScriptableObject
{
    [SerializeField]
    List<ResourceAmount> cost;

    public List<ResourceAmount> Cost { get => cost; }
}
