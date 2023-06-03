using UnityEngine;
using UnityEngine.Events;

/**
 * Exposes the various Clickable mouse events as UnityEvents!
 */
[RequireComponent(typeof(Collider2D))]
public class MouseEvents : AutoMonoBehaviour, IMouseEventReceiver
{
    public bool isClickable = true;
    public bool isDraggable = false;

    public UnityEvent<Vector2> onHover;
    public UnityEvent<Vector2> onHoverExit;
    public UnityEvent<Vector2> onMouseUp;
    public UnityEvent<Vector2> onMouseDown;
    public UnityEvent<Vector2> onMouseDrag;

    public void OnPointerEnter(Vector2 screenPos, Camera cam) {
        onHover.Invoke(screenPos);
    }

    public void OnPointerExit(Vector2 screenPos, Camera cam) {
        onHoverExit.Invoke(screenPos);
    }

    public bool IsMouseInteractableAt(Vector2 screenPos, Camera cam, IMouseAttachable receiver) {
        return isClickable || isDraggable;
    }

    public bool OnPointerDown(Vector2 screenPos, Camera cam) {
        IMouseEventReceiver self = this;
        if (!self.IsMouseInteractableAt(screenPos, cam)) {
            return false;
        }

        if (isClickable) {
            onMouseDown.Invoke(screenPos);
        }
        Debug.Log("you clicked on a thing >:C");

        return true;
    }

    public void OnPointerUp(Vector2 screenPos, Camera cam) {
        if (!isClickable) return;

        onMouseUp.Invoke(screenPos);
    }

    public void OnPointerDrag(Vector2 screenPos, Camera cam) {
        if (!isDraggable) return;

        onMouseDrag.Invoke(screenPos);
    }

    public void SetClickable(bool setter) {
        isClickable = setter;
    }

    public void SetDraggable(bool setter) {
        isDraggable = setter;
    }
}