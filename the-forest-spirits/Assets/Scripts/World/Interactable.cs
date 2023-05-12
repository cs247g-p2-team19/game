using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    public bool isCurrentlyInteractable = true;
    public UnityEvent<Interactable> onInteract;
    
    private Collider2D _collider;

    private void Awake() {
        _collider = GetComponent<Collider2D>();
    }

    public void Interact() {
        onInteract.Invoke(this);
    }
}
