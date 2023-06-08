using UnityEngine;
using UnityEngine.Events;

public class OnAttachedInventoryItemClick : AutoMonoBehaviour, IMouseEventReceiver
{
    public string matchingItemId;

    public UnityEvent onMatch;

    public bool IsMouseInteractableAt(Vector2 screenPos, Camera cam, IMouseAttachable receiver = null) {
        return receiver is AttachableInventoryItem item && item.itemId == matchingItemId;
    }

    public float GetScreenOrdering() {
        return transform.position.z;
    }
}