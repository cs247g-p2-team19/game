using UnityEngine;
using UnityEngine.Events;

/**
 * Defines a component with a trigger-mode Collider2D
 * that the player may Interact with
 */
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
