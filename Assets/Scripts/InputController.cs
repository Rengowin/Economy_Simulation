using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    InputSystem_Actions inputActions;
    [SerializeField]
    BuildingManager buildingManager;
    [SerializeField]
    CameraController cameraController;
    [SerializeField]
    GameController gameController;

    [SerializeField]
    int debugRessource = 0;

    void Awake()
    {
        if(buildingManager == null)
        {
            buildingManager = BuildingManager.Instance;
        }

        if (cameraController == null)
        {
            cameraController = CameraController.Instance;
        }
        if (gameController == null)
        {
            gameController = GameController.Instance;
        }

        inputActions = new InputSystem_Actions();

        inputActions.BuildModus.SpawnWoodCutter.performed += ctx => SpawnWoodCutter();
        inputActions.BuildModus.SpawnSawMill.performed += ctx => SpawnSawMill();
        inputActions.BuildModus.SpawnHut.performed += ctx => SpawnHut();
        inputActions.BuildModus.GiveWood.performed += ctx => GiveWood();

        inputActions.Core.ChangeTickSpeed.performed += ctx => ChangeTickSpeed((int)ctx.ReadValue<float>());
        inputActions.Core.Pause.performed += ctx => PauseGame();
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

    void Update()
    {
        // sind in update weil die immer wiederholt aufgerufen werden sollen (aber sollte das in in awake reichen? die performed bzw cxt zu setzten?)
        Vector2 move = inputActions.Camera.Move.ReadValue<Vector2>();
        float rotate = inputActions.Camera.Rotate.ReadValue<float>();
        float zoom = inputActions.Camera.Zoom.ReadValue<float>();

        cameraController.Move(move);
        cameraController.Rotate(rotate);
        cameraController.Zoom(zoom);
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

    void ChangeTickSpeed(int change)
    {
        gameController.ChangeTimeScale(change);
    }

    void PauseGame()
    {
        gameController.PauseGame();
    }
}
