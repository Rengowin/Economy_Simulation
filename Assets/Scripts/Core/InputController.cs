using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    public static InputController Instance;

    InputSystem_Actions inputActions;
    [SerializeField]
    BuildingManager buildingManager;
    [SerializeField]
    CameraController cameraController;
    [SerializeField]
    GameController gameController;

    [SerializeField]
    ResourceType debugRessource;
    [SerializeField]
    int debugRessourceAmount = 0;

    BuildingData selectedBuilding;
    public BuildingData SelectedBuilding { get => selectedBuilding; set => selectedBuilding = value; }
    //getter for later (idk if needed for a "shader" that shows can buildd or not)
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of InputController found!");
            Destroy(this);
        }


        if (buildingManager == null)
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

        inputActions.BuildModus.GiveWood.performed += ctx => GiveWood();

        inputActions.BuildModus.AktivePlacement.performed += ctx => SpawnSelectedBuilding();
        inputActions.BuildModus.CancelPlacement.performed += ctx => CancelPlacement();

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

    void SpawnSelectedBuilding()
    {
        if(selectedBuilding == null)
        {
            return;
        }
        
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Mouse is over UI, not spawning building.");
            return;
        }

        if(TryGetMouseWorldPosition(out Vector3 pos))
        {
            buildingManager.BuildBuilding(selectedBuilding, pos);
        }
    }

    void CancelPlacement()
    {
        selectedBuilding = null;
    }

    void GiveWood()
    {
        StorageManager.Instance.GlobalStorage.Add(debugRessource, debugRessourceAmount);
    }

    //for now public, will be later sourced out? (since the validation needs also position or i find an other way)
    public bool TryGetMouseWorldPosition(out Vector3 position)
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
