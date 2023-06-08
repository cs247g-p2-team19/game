using UnityEngine;

/**
 * Defines an object that can receive mouse events.
 */
public interface IMouseEventReceiver
{
    /**
     * If there are only sub-parts of this object that are clickable,
     * this lets you implement that. Return true if the object is
     * clickable at the current screenPosition, else false.
     *
     * By default, returns true iff there's no receiver.
     */
    public bool IsMouseInteractableAt(Vector2 screenPos, Camera cam, IMouseAttachable receiver = null) {
        return receiver == null;
    }

    /**
     * This event fires when the mouse is clicked down. Returns true
     * if the event was handled, and false otherwise.
     *
     * By default, always returns false.
     */
    public bool OnPointerDown(Vector2 screenPos, Camera cam) {
        return true;
    }

    /**
     * This event fires when the mouse is released. Returns true
     * if the event was handled, and false otherwise.
     *
     * By default, always returns false.
     */
    public void OnPointerUp(Vector2 screenPos, Camera cam) { }

    /**
     * This event fires when the cursor appears over any area
     * where IsMouseInteractableAt returns true.
     */
    public void OnPointerEnter(Vector2 screenPos, Camera cam) { }

    /**
     * This event fires when the cursor leaves any area
     * where IsMouseInteractableAt returns true.
     */
    public void OnPointerExit(Vector2 screenPos, Camera cam) { }

    /**
     * This even fire when the cursor is down and drags; return false on default
     */
    public void OnPointerDrag(Vector2 screenPos, Camera cam) { }

    /**
     * Allows mouseovers to override the mouse animations while hovering.
     */
    public AnimatorOverrideController GetCustomAnimation() {
        return null;
    }

    public float GetScreenOrdering();
}