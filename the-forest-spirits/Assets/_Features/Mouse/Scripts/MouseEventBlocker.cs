using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MouseEventBlocker : AutoMonoBehaviour, IMouseEventReceiver
{
    public bool IsMouseInteractableAt(Vector2 screenPos, Camera cam, IMouseAttachable receiver = null) {
        return true;
    }

    public float GetScreenOrdering() {
        return transform.position.z;
    }
}