using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * Defines an item that can later be picked up.
 * It should be attached to a GameObject in the Inventory, which
 * will be activated when an associated Collectable is picked up
 * (or Unlock is called on this InventoryItem).
 */
public class InventoryItem : MonoBehaviour
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
    public string itemName;  // Might be unnecessary?
    [Tooltip("Whether or not the user starts with this item. True if they start without it.")]
    public bool startLocked = true;
    
    [Header("Events")]
    public UnityEvent<InventoryItem> onUnlock;
    
    [Tooltip("Invoked when this is clicked in the inventory.")]
    public UnityEvent<InventoryItem> onClick;
    
    #endregion

    private void Awake() {
        Debug.Assert(!_registry.ContainsKey(itemId), "Multiple items with the same ID detected!");
        Debug.Assert(itemId != "", "Item ID may not be blank!");

        _registry[itemId] = this;

        if (startLocked) {
            Lock();
        }
    }
    

    public void Unlock() {
        gameObject.SetActive(true);
        onUnlock.Invoke(this);
        Lil.Guy.onUnlockItem.Invoke(this);
    }

    public void Lock() {
        gameObject.SetActive(false);
    }
    
    // TODO on click
}