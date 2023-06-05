using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

/**
 * Defines an item that can later be picked up.
 * It should be attached to a GameObject in the Inventory, which
 * will be activated when an associated Collectable is picked up
 * (or Unlock is called on this InventoryItem).
 */
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(MatchColliderToRectTransform))]
[RequireComponent(typeof(RectTransform))]
public class InventoryItem : AutoMonoBehaviour, IMouseEventReceiver
{
    #region Registry

    // Since we reference InventoryItems by their IDs, we need a registry
    private static Dictionary<string, InventoryItem> _registry = new();

    public static InventoryItem GetItemById(string id) {
        return _registry.TryGetValue(id, out var item) ? item : null;
    }

    #endregion

    #region Unity fields

    [Tooltip("Unique item ID for this item. Can be referenced elsewhere.")]
    public string itemId;

    public AudioClip onUnlockSound;
    public AudioClip onUseSound;

    [FormerlySerializedAs("startLocked")]
    [Tooltip("Whether or not the user starts with this item. True if they start without it.")]
    public bool isLocked = true;

    [Header("Events")]
    public UnityEvent<InventoryItem> onUnlock;

    [FormerlySerializedAs("onClick")]
    [Tooltip("Invoked when this is used in the inventory.")]
    public UnityEvent<InventoryItem> onUse;

    [Tooltip("Is this a note. Never before seen functionality")]
    public bool isNote;

    #endregion

    private bool _setupDone = false;
    
    private void OnDestroy() {
        _registry.Remove(itemId);
    }

    /** Sets up and registers this InventoryItem */
    public void Setup() {
        if (_setupDone) return;

        _setupDone = true;
        Debug.Assert(!_registry.ContainsKey(itemId), "Multiple items with the same ID" + itemId + " detected!");
        Debug.Assert(itemId != "", "Item ID may not be blank!");
        Debug.Log("mfw I am getting setup" + itemId);

        _registry[itemId] = this;

        if (isLocked) {
            Lock();
        }
    }

    private void Awake() {
        Setup();
    }

    public virtual bool OnPointerDown(Vector2 _, Camera __) {
        if (onUseSound != null) {
            Lil.Guy.PlaySFX(onUseSound);
        }

        onUse.Invoke(this);
        return true;
    }

    public virtual bool IsMouseInteractableAt(Vector2 screenPos, Camera cam, IMouseAttachable receiver) {
        return onUse.GetPersistentEventCount() > 0 && receiver == null;
    }

    public void Unlock() {
        if (onUnlockSound != null) {
            Lil.Guy.PlaySFX(onUnlockSound);
        }

        gameObject.SetActive(true);
        onUnlock.Invoke(this);
        Lil.Guy.onUnlockItem.Invoke(this);
        this.isLocked = false;
        Lil.Inventory.RenderInventory();
    }

    public void Lock() {
        gameObject.SetActive(false);
        Lil.Inventory.RenderInventory();
        this.isLocked = true;
    }
}