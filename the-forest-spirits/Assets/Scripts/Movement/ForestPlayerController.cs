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

    [Tooltip("The controls")]
    public InputActionAsset actions;

    public Animator animator;

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


    #region Input Actions & Maps

    private InputActionMap _map;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _interactAction;
    private InputAction _dashAction;
    private InputAction _inventoryAction;

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

        _jumpAction.performed += OnJump;
        _interactAction.performed += OnInteract;
        _inventoryAction.performed += OnInventory;

        _map.Enable();
    }

    private void OnDisable() {
        _jumpAction.performed -= OnJump;
        _interactAction.performed -= OnInteract;
        _inventoryAction.performed -= OnInventory;

        _map.Disable();
    }

    /** Movement happens here */
    void Update() {
        mouseManager.UpdateMouse(_pointerLocation.ReadValue<Vector2>(), _interactAction.IsPressed());
        

        if (Lil.Inventory.IsOpen || _stopped) return;

        float direction = _moveAction.ReadValue<float>();
        bool dashing = _dashAction.IsPressed();
        float moveSpeed = dashing ? dashSpeed : speed;

        animator.SetFloat(Speed, Mathf.Abs(direction * moveSpeed));

        transform.position += Vector3.right * (direction * moveSpeed * Time.deltaTime);
    }

    private void OnJump(InputAction.CallbackContext context) {
        if (Lil.Inventory.IsOpen || _stopped) return;

        Debug.Log("Jumped!");
    }

    private void OnInteract(InputAction.CallbackContext context) {
        if (Lil.Inventory.IsOpen) return;

        Vector2 cameraPos = _pointerLocation.ReadValue<Vector2>();
        Ray toCast = mainCamera.ScreenPointToRay(cameraPos);
        var hit = Physics2D.Raycast(toCast.origin, toCast.direction, Mathf.Infinity, LayerMask.GetMask("Clickables"));
        IClickable clickable = null;

        if (hit.collider != null) {
            clickable = hit.collider.GetComponentInParent<IClickable>();
        }

        if (clickable != null && clickable.OnClick(cameraPos, mainCamera)) {
            return;
        }

        Lil.Guy.TriggerInteractions();
    }

    private void OnInventory(InputAction.CallbackContext context) {
        if (_stopped) return;

        Debug.Log("Triggered inventory");

        Lil.Inventory.Toggle();

        animator.SetBool(InventoryOpen, Lil.Inventory.IsOpen);

        if (Lil.Inventory.IsOpen) {
            cameraFocus.Focus(inventoryFocus);
        }
        else {
            cameraFocus.Unfocus(inventoryFocus);
        }
    }

    public void Lmao() {
        Debug.Log("Pog");
    }
}