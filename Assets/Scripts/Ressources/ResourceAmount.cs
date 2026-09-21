using System;
using UnityEngine;

[Serializable]
public class ResourceAmount
{
    [SerializeField]
    RessourceEnum ressource;
    [SerializeField]
    int amount;

    public RessourceEnum Ressource { get => ressource;}
    public int Amount { get => amount;}
}
