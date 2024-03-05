using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler Instance { get; private set; }

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string pickup = "PickUp";
    [SerializeField] private string zoomin = "ZoomIn";
    [SerializeField] private string zoomout = "ZoomOut";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        pickupAction = playerControls.FindActionMap(actionMapName).FindAction(pickup);
        zoominAction = playerControls.FindActionMap(actionMapName).FindAction(zoomin);
        zoomoutAction = playerControls.FindActionMap(actionMapName).FindAction(zoomout);

        RegisterInputActions();

        PrintDevices();
    }

    private void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;
        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;
        pickupAction.performed += context => PickingUp = true;
        pickupAction.canceled += context => PickingUp = false;
        zoominAction.performed += context => ZoomIn = true;
        zoominAction.canceled += context => ZoomIn = false;
        zoomoutAction.performed += context => ZoomOut = true;
        zoomoutAction.canceled += context => ZoomOut = false;
    }

    private void PrintDevices()
    {
        foreach (var device in InputSystem.devices)
        {
            if (device.enabled)
            {
                Debug.Log("Active Device: " + device.name);
            }
        }
    }
    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        pickupAction.Enable();
        zoominAction.Enable();
        zoomoutAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        pickupAction.Disable();
        zoominAction.Disable();
        zoomoutAction.Disable();
    }

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool PickingUp { get; private set; }
    public bool ZoomIn { get; private set; }
    public bool ZoomOut { get; private set; }

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction pickupAction;
    private InputAction zoominAction;
    private InputAction zoomoutAction;
}
