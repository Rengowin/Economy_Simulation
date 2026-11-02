using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceType", menuName = "Scriptable Objects/ResourceType")]
public class ResourceType : ScriptableObject
{
    [SerializeField]
    string resourceName;
    [SerializeField]
    Sprite resourceIcon;

    public string ResourceName => resourceName;
    public Sprite ResourceIcon => resourceIcon;
}
