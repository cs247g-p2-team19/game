using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Implements all the main movement of the player.
 */
public class ForestPlayerController : PlayerController
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int InventoryOpen = Animator.StringToHash("InventoryOpen");

    [Tooltip("The controls")]
    public InputActionAsset actions;

    public Animator animator;

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
    
    #endregion

    
    private void OnEnable() {
        _map = actions.FindActionMap("ForestMovement");
        _moveAction = _map.FindAction("Move");
        _jumpAction = _map.FindAction("Jump");
        _interactAction = _map.FindAction("Interact");
        _dashAction = _map.FindAction("Dash");
        _inventoryAction = _map.FindAction("Open Inventory");

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
        if (Lil.Inventory.IsOpen || _stopped) return;

        float direction =  _moveAction.ReadValue<float>();
        bool dashing = _dashAction.IsPressed();
        float moveSpeed = dashing ? dashSpeed : speed;
        
        animator.SetFloat(Speed, Mathf.Abs(direction * moveSpeed));

        transform.position += Vector3.right *  (direction * moveSpeed * Time.deltaTime);
    }

    private void OnJump(InputAction.CallbackContext context) {
        if (Lil.Inventory.IsOpen || _stopped) return;

        Debug.Log("Jumped!");
    }

    private void OnInteract(InputAction.CallbackContext context) {
        if (Lil.Inventory.IsOpen) return;

        Lil.Guy.TriggerInteractions();
    }

    private void OnInventory(InputAction.CallbackContext context) {
        if (_stopped) return;
        
        Debug.Log("Triggered inventory");

        Lil.Inventory.Toggle();
        
        animator.SetBool(InventoryOpen, Lil.Inventory.IsOpen);
    }
}