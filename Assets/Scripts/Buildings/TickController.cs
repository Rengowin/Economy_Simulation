using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class TickController : MonoBehaviour
{
    //Instance
    public static TickController Instance;

    //all ITickables
    //ist list sinnvoll? weil das ja am ende ein O(N) wird
    List<ITickable> stuffThatsNeedUpdates;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            stuffThatsNeedUpdates = new List<ITickable>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        SendUpdate(Time.deltaTime);
    }

    public void Register(ITickable tickable)
    {
        if(!stuffThatsNeedUpdates.Contains(tickable))
        {
            stuffThatsNeedUpdates.Add(tickable);
        }
    }

    public void Unregister(ITickable tickable)
    {
        stuffThatsNeedUpdates.Remove(tickable);
    }

    void SendUpdate(float deltaTime)
    {
        foreach (ITickable tickable in stuffThatsNeedUpdates)
        {
            tickable.GameTick(deltaTime);
        }
    }
}
