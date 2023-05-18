using UnityEngine;
using UnityEngine.Events;

/**
 * Exposes the various Clickable mouse events as UnityEvents!
 */
[RequireComponent(typeof(Collider2D))]
public class MouseEvents : AutoMonoBehaviour, IMouseEventReceiver
{
    public bool isClickable = true;

    public UnityEvent onHover;
    public UnityEvent onHoverExit;
    public UnityEvent onMouseUp;
    public UnityEvent onMouseDown;

    public void OnPointerEnter(Vector2 screenPos, Camera cam) {
        onHover.Invoke();
    }

    public void OnPointerExit(Vector2 screenPos, Camera cam) {
        onHoverExit.Invoke();
    }

    public bool IsMouseInteractableAt(Vector2 screenPos, Camera cam) {
        return isClickable;
    }

    public bool OnPointerDown(Vector2 screenPos, Camera cam) {
        if (onMouseDown.GetPersistentEventCount() == 0) return false;
        onMouseDown.Invoke();
        return true;
    }
    
    public bool OnPointerUp(Vector2 screenPos, Camera cam) {
        if (onMouseUp.GetPersistentEventCount() == 0) return false;
        onMouseUp.Invoke();
        return true;
    }
}