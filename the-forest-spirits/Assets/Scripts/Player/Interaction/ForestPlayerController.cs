using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

/**
 * Implements all the main movement of the player.
 */
[RequireComponent(typeof(MouseManager))]
public class ForestPlayerController : PlayerController
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int InventoryOpen = Animator.StringToHash("InventoryOpen");

    public override Vector2 Velocity { get; protected set; }

    [Tooltip("The controls"), Required]
    public InputActionAsset actions;

    [Required]
    public Animator animator;

    [Required]
    public CameraFocusArea inventoryFocus;

    [ReadOnly]
    public CameraFocuser cameraFocus;

    [AutoDefaultMainCamera, ReadOnly]
    public Camera mainCamera;

    [AutoDefault, ReadOnly]
    public MouseManager mouseManager;

    [Tooltip("Standard speed of the ghost")]
    public float speed = 5f;

    public float dashSpeed = 10f;

    private Vector2 _currentVelocity;


    #region Input Actions & Maps

    private InputActionMap _map;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _interactAction;
    private InputAction _dashAction;
    private InputAction _inventoryAction;
    private InputAction _escapeAction;

    private InputAction _pointerLocation;

    #endregion

    protected override void OnValidate() {
        base.OnValidate();
        if (!cameraFocus.IsUnityNull() || mainCamera.IsUnityNull()) return;
        cameraFocus = mainCamera.GetComponent<CameraFocuser>();
    }

    private void OnEnable() {
        _map = actions.FindActionMap("ForestMovement");
        _moveAction = _map.FindAction("Move");
        _jumpAction = _map.FindAction("Jump");
        _interactAction = _map.FindAction("Interact");
        _dashAction = _map.FindAction("Dash");
        _inventoryAction = _map.FindAction("Inventory");
        _pointerLocation = _map.FindAction("PointerLocation");
        _escapeAction = _map.FindAction("Escape");

        _jumpAction.performed += OnJump;
        _inventoryAction.performed += OnInventory;
        _escapeAction.performed += OnEscape;

        _map.Enable();
    }

    private void OnDisable() {
        _jumpAction.performed -= OnJump;
        _inventoryAction.performed -= OnInventory;
        _escapeAction.performed -= OnEscape;

        _map.Disable();
    }

    /** Movement happens here */
    void Update() {
        mouseManager.UpdateMouse(_pointerLocation.ReadValue<Vector2>(), _interactAction.IsPressed());
        

        if (Lil.Inventory.IsOpen || _stopped) return;

        float direction = _moveAction.ReadValue<float>();
        bool dashing = _dashAction.IsPressed();
        float moveSpeed = dashing ? dashSpeed : speed;
        Velocity = new Vector2(moveSpeed, 0);

        animator.SetFloat(Speed, Mathf.Abs(direction * moveSpeed));

        transform.position += Vector3.right * (direction * moveSpeed * Time.deltaTime);
    }

    private void OnJump(InputAction.CallbackContext context) {
        if (Lil.Inventory.IsOpen || _stopped) return;

        Debug.Log("Jumped!");
    }
    
    private void OnInventory(InputAction.CallbackContext context) {
        if (_stopped) return;
        
        Lil.Inventory.Toggle();

        animator.SetBool(InventoryOpen, Lil.Inventory.IsOpen);

        if (Lil.Inventory.IsOpen) {
            cameraFocus.Focus(inventoryFocus);
        }
        else {
            cameraFocus.Unfocus(inventoryFocus);
        }
    }

    private void OnEscape(InputAction.CallbackContext context) {
        EscapeStack.Instance.DoEscape();
    }
}