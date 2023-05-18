using UnityEngine;

public interface IClickable
{
    public bool IsMouseInteractableAt(Vector2 screenPos, Camera cam) {
        return true;
    }

    public bool OnPointerDown(Vector2 screenPos, Camera cam) {
        return false;
    }
    
    public bool OnPointerUp(Vector2 screenPos, Camera cam) {
        return false;
    }

    public AnimatorOverrideController GetCustomAnimation() {
        return null;
    }

    public void OnPointerEnter(Vector2 screenPos, Camera cam) { }
    public void OnPointerExit(Vector2 screenPos, Camera cam) { }
}