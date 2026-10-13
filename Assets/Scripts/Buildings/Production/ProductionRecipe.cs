using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Production Recipe", menuName = "Scriptable Objects/Production Recipe")]
public class ProductionRecipe : ScriptableObject
{
    [SerializeField]
    List<ResourceAmount> input;

    [SerializeField]
    List<ResourceAmount> output;

    [SerializeField]
    float productionTime;

    public List<ResourceAmount> Input { get => input; }
    public List<ResourceAmount> Output { get => output; }
    public float ProductionTime { get => productionTime; }
}
