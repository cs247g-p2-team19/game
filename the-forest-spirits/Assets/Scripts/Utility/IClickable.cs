using UnityEngine;

public interface IClickable
{
    
    public bool IsMouseInteractableAt(Vector2 screenPos, Camera cam, out AnimatorOverrideController customAnimator);

    public bool OnMouseClick(Vector2 screenPos, Camera cam) {
        return false;
    }


    public void OnMouseEnter(Vector2 screenPos, Camera cam) { }
    public void OnMouseExit(Vector2 screenPos, Camera cam) { }
}