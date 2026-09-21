using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    InputSystem_Actions inputActions;
    [SerializeField]
    BuildingManager buildingManager;

    void Awake()
    {
        inputActions = new InputSystem_Actions();

        inputActions.BuildModus.SpawnWoodCutter.performed += ctx => SpawnWoodCutter();
        inputActions.BuildModus.SpawnSawMill.performed += ctx => SpawnSawMill();
        inputActions.BuildModus.SpawnHut.performed += ctx => SpawnHut();
    }

    void Start()
    {
        if(BuildingManager.Instance == null)
        {
            Debug.LogError("BuildingManager is not set in InputController");
        }
        else
        {
            buildingManager = BuildingManager.Instance;
        }
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void SpawnWoodCutter()
    {
        buildingManager.SpawnTestWoodCutter(new Vector3(0, 0, 0));
    }

    void SpawnSawMill()
    {
        buildingManager.SpawnTestSawMill(new Vector3(5, 0, 0));
    }

    void SpawnHut()
    {
        buildingManager.SpawnTestHut(new Vector3(10, 0, 0));
    }
}
