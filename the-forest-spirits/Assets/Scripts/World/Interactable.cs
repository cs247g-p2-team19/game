using UnityEngine;
using UnityEngine.Events;

/**
 * Defines a component with a trigger-mode Collider2D
 * that the player may Interact with
 */
[RequireComponent(typeof(Collider2D))]
public class Interactable : AutoMonoBehaviour
{
    public bool isCurrentlyInteractable = true;
    public UnityEvent<Interactable> onInteract;
    
    [AutoDefault]
    public new Collider2D collider;


    public void Interact() {
        onInteract.Invoke(this);
    }
}
