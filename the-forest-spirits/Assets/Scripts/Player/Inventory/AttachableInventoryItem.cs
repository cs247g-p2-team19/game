using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AttachableInventoryItem : InventoryItem, IMouseAttachable
{
    public Sprite spriteToAttach;

    public UnityEvent onMatch;

    [SerializeField, Required]
    private GameObject _spriteAttachable;

    protected override void OnValidate() {
        base.OnValidate();
        if (isNote) {
            Debug.LogError("An attachable inventory item cannot be a note!", this);
        }
    }


    public override bool IsMouseInteractableAt(Vector2 screenPos, Camera cam, IMouseAttachable receiver) {
        return receiver == null;
    }

    public override bool OnPointerDown(Vector2 _, Camera __) {
        return true;
    }

    public bool OnTryAttach(MouseManager manager) {
        Debug.Log("Attaching!");
        var attachment = manager.SetCursorAttachment(_spriteAttachable);
        var image = attachment.GetComponentInChildren<Image>();
        image.sprite = spriteToAttach;
        var scaler = attachment.GetComponentInChildren<AspectRatioFitter>();
        scaler.aspectRatio = spriteToAttach.bounds.size.x / spriteToAttach.bounds.size.y;
        return true;
    }

    public bool OnClickWhileAttached(List<IMouseEventReceiver> others, MouseManager manager) {
        Debug.Log("Clicked while attached!");
        OnAttachedInventoryItemClick match = others.OfType<OnAttachedInventoryItemClick>().ElementAtOrDefault(0);
        manager.RemoveCursorAttachment();
        if (match == null) return false;
        onMatch.Invoke();
        match.onMatch.Invoke();
        return true;
    }
}