using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ForestMovementController : MonoBehaviour
{
    public InputActionAsset actions;
    public float speed = 5f;
    public float dashSpeed = 10f;

    private InputActionMap _map;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _interactAction;
    private InputAction _dashAction;

    void Awake() {
        _map = actions.FindActionMap("ForestMovement");
        _moveAction = _map.FindAction("Move");
        _jumpAction = _map.FindAction("Jump");
        _interactAction = _map.FindAction("Interact");
        _dashAction = _map.FindAction("Dash");

        _jumpAction.performed += OnJump;
        _interactAction.performed += OnInteract;
    }

    private void OnEnable() {
        _map.Enable();
    }

    private void OnDisable() {
        _map.Disable();
    }

    // Update is called once per frame
    void Update() {
        Vector3 movement = Vector3.right * _moveAction.ReadValue<float>();
        bool dashing = _dashAction.IsPressed();
        float moveSpeed = dashing ? dashSpeed : speed;

        transform.position += movement * (moveSpeed * Time.deltaTime);
    }

    private void OnJump(InputAction.CallbackContext context) {
        Debug.Log("Jumped!");
    }

    private void OnInteract(InputAction.CallbackContext context) {
        Debug.Log("Interacted");
    }
}