using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ForestMovementController : MonoBehaviour
{
    public InputActionAsset actions;
    public float speed = 1.0f;

    private InputActionMap _map;
    
    private InputAction _moveAction;

    private InputAction _jumpAction;

    private InputAction _interactAction;
    
    void Awake()
    {
        _map = actions.FindActionMap("ForestMovement");
        _moveAction = _map.FindAction("Move");
        _jumpAction = _map.FindAction("Jump");

        _jumpAction.performed += OnJump;

        _interactAction = _map.FindAction("Interact");
        _interactAction.performed += OnInteract;
    }

    private void OnEnable()
    {
        _map.Enable();
    }

    private void OnDisable()
    {
        _map.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = _moveAction.ReadValue<Vector2>();

        transform.position += movement * (speed * Time.deltaTime);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumped!");
    }

    private void OnInteract(InputAction.CallbackContext context) {
        Debug.Log("Interaction");
    }
}
