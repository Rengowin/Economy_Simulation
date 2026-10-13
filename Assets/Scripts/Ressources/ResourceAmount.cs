using System;
using UnityEngine;

[Serializable]
public class ResourceAmount
{
    [SerializeField]
    ResourceType ressource;
    [SerializeField]
    int amount;

    public ResourceType Ressource { get => ressource;}
    public int Amount { get => amount;}
}
