using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    InputSystem_Actions inputActions;
    [SerializeField]
    BuildingManager buildingManager;

    [SerializeField]
    int debugRessource = 0;

    void Awake()
    {
        inputActions = new InputSystem_Actions();

        inputActions.BuildModus.SpawnWoodCutter.performed += ctx => SpawnWoodCutter();
        inputActions.BuildModus.SpawnSawMill.performed += ctx => SpawnSawMill();
        inputActions.BuildModus.SpawnHut.performed += ctx => SpawnHut();
        inputActions.BuildModus.GiveWood.performed += ctx => GiveWood();
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
        if (TryGetMouseWorldPosition(out Vector3 position))
        {
            buildingManager.SpawnTestWoodCutter(position);
        }
    }

    void SpawnSawMill()
    {
        if (TryGetMouseWorldPosition(out Vector3 position))
        {
            buildingManager.SpawnTestSawMill(position);
        }
    }

    void SpawnHut()
    {
        if (TryGetMouseWorldPosition(out Vector3 position))
        {
            buildingManager.SpawnTestHut(position);
        }
    }

    void GiveWood()
    {
        StorageManager.Instance.GlobalStorage.Add(ResourceType.Wood, debugRessource);
    }


    //Vector3 GetMouseWorldPosition()
    //{
    //    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
    //    Vector2 mousePosition = Mouse.current.position.ReadValue();
    //    Ray ray = Camera.main.ScreenPointToRay(mousePosition);

    //    //if (groundPlane.Raycast(ray, out RaycastHit hit))
    //    //{
    //    //    return hit.point;
    //    //}

    //    if (groundPlane.Raycast(ray, out float enter))
    //    {
    //        return ray.GetPoint(enter);
    //    }
    //    else
    //    {
    //        Debug.LogError("Mouse ray did not hit the ground plane.");
    //        return Vector3.zero;
    //    }
    //}

    bool TryGetMouseWorldPosition(out Vector3 position)
    {
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (groundPlane.Raycast(ray, out float distance))
        {
            position = ray.GetPoint(distance);
            return true;
        }

        Debug.LogError("Mouse ray did not hit the ground plane.");
        position = Vector3.zero;
        return false;
    }
}
